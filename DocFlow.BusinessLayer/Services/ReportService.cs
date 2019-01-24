using DocFlow.BusinessLayer.Interfaces;
using DocFlow.BusinessLayer.Models.FlowWordFile;
using DocFlow.BusinessLayer.Models.Report;
using DocFlow.Data;
using DocFlow.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using DocFlow.BusinessLayer.Models.Report.History;

namespace DocFlow.BusinessLayer.Services
{
    public class ReportService : IReportService
    {
        public readonly DocFlowCotext _docFlowContext;
        private readonly IMapper _mapper;

        public ReportService(DocFlowCotext docFlowContext, IMapper mapper)
        {
            _docFlowContext = docFlowContext;
            _mapper = mapper;

        }
        public List<ReportLabel> GetReportLabels(int reportTypeId)
        {
            return _docFlowContext.ReportLabels.Where(x => x.ReportTypeId == reportTypeId)
                .ToList();
        }

        public List<ReportType> GetReportTypes()
        {
            return _docFlowContext.ReportTypes.ToList();
        }

        public async Task GenerateReportAsync(GenerateReportModel generateReport, Guid userId, string nameFile)
        {
            try
            {
                nameFile = Helpers.Helpers.GetNameFileWithCurrentDate(nameFile);

                Report report = new Report
                {
                    ReportTypeId = generateReport.ReportTypeId,
                    SignerUserId = userId,
                    Name = nameFile
                };

                await _docFlowContext.Reports.AddAsync(report);

                ReportHistory reportHistory = new ReportHistory
                {
                    ReportId = report.Id,
                    CreateUserId = userId,
                    CreateDate = DateTime.Now
                };

                await _docFlowContext.ReportHistory.AddAsync(reportHistory);

                foreach (ReportLabelModel label in generateReport.Values)
                {
                    ReportValue reportValue = new ReportValue
                    {
                        ReportId = report.Id,
                        ReportLabelId = label.Id,
                        Value = label.Value
                    };

                   await _docFlowContext.ReportValues.AddAsync(reportValue);

                    ReportValuesHistory reportValuesHistory = new ReportValuesHistory
                    {
                        ReportValueId = reportValue.Id,
                        ReportHistoryId = reportHistory.Id,
                        NewValue = reportValue.Value
                    };

                    await _docFlowContext.ReportValuesHistory.AddAsync(reportValuesHistory);
                }

                await _docFlowContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void GenerateReport(GenerateReportModel generateReport, Guid userId, string nameFile)
        {
            try
            {
                nameFile = Helpers.Helpers.GetNameFileWithCurrentDate(nameFile);

                Report report = new Report
                {
                    ReportTypeId = generateReport.ReportTypeId,
                    SignerUserId = userId,
                    Name = nameFile
                };

                _docFlowContext.Reports.Add(report);

                ReportHistory reportHistory = new ReportHistory
                {
                    ReportId = report.Id,
                    CreateUserId = userId,
                    CreateDate = DateTime.Now
                };

                _docFlowContext.ReportHistory.Add(reportHistory);

                foreach (ReportLabelModel label in generateReport.Values)
                {
                    ReportValue reportValue = new ReportValue
                    {
                        ReportId = report.Id,
                        ReportLabelId = label.Id,
                        Value = label.Value
                    };

                    _docFlowContext.ReportValues.Add(reportValue);

                    ReportValuesHistory reportValuesHistory = new ReportValuesHistory
                    {
                        ReportValueId = reportValue.Id,
                        ReportHistoryId = reportHistory.Id,
                        NewValue = reportValue.Value
                    };

                    _docFlowContext.ReportValuesHistory.Add(reportValuesHistory);
                }

                _docFlowContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public List<ReportHistory> GetReportHistory(int reportId)
        {
            return _docFlowContext.ReportHistory.Include(i => i.CreateUser).Where(x=>x.ReportId == reportId).OrderByDescending(x => x.CreateDate).ToList();
        }
        
         public ReportHistoryViewModel GetReportHistoryValues(int reportHistoryId)
        {
            var reportHistory = _docFlowContext.ReportHistory.Include(i => i.Report).Include(i=> i.ValuesHistory).ThenInclude(t=> t.ReportValue).ThenInclude(f=>f.ReportLabel).FirstOrDefault(x => x.Id == reportHistoryId);
            var items = reportHistory.ValuesHistory.Select(_mapper.Map<ReportValuesHistory, ReportValuesHistoryViewModel>).ToList();
            var res = new ReportHistoryViewModel()
            {
                Id = reportHistory.Id,
                CreateDate = reportHistory.CreateDate,
                ReportName = reportHistory.Report.Name,
                ReportId = reportHistory.Report.Id,
                Values = items
            };
            return res;
        }
 

        public List<Report> GetReports()
        {
            return _docFlowContext.Reports.Include(i => i.SignerUser).OrderByDescending(x => x.Id).ToList();
        }

        public Report GetReport(int reportId)
        {
            return _docFlowContext.Reports
                    .Include(i => i.ReportType)
                    .Include(i => i.SignerUser)
                    .Include(i => i.Values)
                        .ThenInclude(t => t.ReportLabel)
                .FirstOrDefault(r => r.Id == reportId);
        }

        public async Task UpdateReportAsync(UpdateReportModel updateReport, Guid userId)
        {
            try
            {
                ReportHistory reportHistory = new ReportHistory
                {
                    ReportId = updateReport.ReportId,
                    CreateUserId = userId,
                    CreateDate = DateTime.Now
                };

                await _docFlowContext.ReportHistory.AddAsync(reportHistory);

                List<ReportValue> reportValues = GetReportValues(updateReport.ReportId);

                foreach (ReportValue reportValue in reportValues)
                {
                    ReportLabelModel reportLabelModel = updateReport.Values.FirstOrDefault(x => x.Id == reportValue.ReportLabelId);

                    await _docFlowContext.ReportValuesHistory.AddAsync(new ReportValuesHistory
                    {
                        ReportValueId = reportValue.Id,
                        ReportHistoryId = reportHistory.Id,
                        OldValue = reportValue.Value,
                        NewValue = reportLabelModel.Value
                    });

                    reportValue.Value = reportLabelModel.Value;
                }

                await _docFlowContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void UpdateReport(UpdateReportModel updateReport, Guid userId)
        {
            try
            {
                ReportHistory reportHistory = new ReportHistory
                {
                    ReportId = updateReport.ReportId,
                    CreateUserId = userId,
                    CreateDate = DateTime.Now
                };

                _docFlowContext.ReportHistory.Add(reportHistory);

                List<ReportValue> reportValues = GetReportValues(updateReport.ReportId);

                foreach (ReportValue reportValue in reportValues)
                {
                    ReportLabelModel reportLabelModel = updateReport.Values.FirstOrDefault(x => x.Id == reportValue.ReportLabelId);

                    _docFlowContext.ReportValuesHistory.Add(new ReportValuesHistory
                    {
                        ReportValueId = reportValue.Id,
                        ReportHistoryId = reportHistory.Id,
                        OldValue = reportValue.Value,
                        NewValue = reportLabelModel.Value
                    });

                    reportValue.Value = reportLabelModel.Value;
                }

                _docFlowContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public List<ReportValue> GetReportValues(int reportId)
        {
            return _docFlowContext.ReportValues.Where(x => x.ReportId == reportId).ToList();
        }

        public async Task SignedReport(int reportId)
        {
            try
            {
                Report report = GetReport(reportId);
                report.IsSigned = true;

                await _docFlowContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
