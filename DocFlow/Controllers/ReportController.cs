using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DocFlow.BusinessLayer.Enums;
using DocFlow.BusinessLayer.Helpers;
using DocFlow.BusinessLayer.Interfaces;
using DocFlow.BusinessLayer.Interfaces.GraphServices;
using DocFlow.BusinessLayer.Models.FlowWordFile;
using DocFlow.BusinessLayer.Models.Report;
using DocFlow.BusinessLayer.Models.Report.History;
using DocFlow.Data;
using DocFlow.Data.Entities;
using DocFlow.Models.FlowWordFile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReportController : BaseController
    {
        private readonly IReportService _reportService;
        public readonly IDocumentService _documentService;
        public readonly IGraphServiceFiles _graphServiceFiles;

        public ReportController(
                DocFlowCotext context,
                IReportService reportService,
                IDocumentService documentService,
                IGraphServiceFiles graphServiceFiles
            ) : base(context)
        {
            _reportService = reportService;
            _documentService = documentService;
            _graphServiceFiles = graphServiceFiles;
        }

        #region Helpers

        private async Task PushFilesToDrive(string nameFile, int reportTypeId, List<ReportLabelModel> values, int driveType)
        {
            nameFile = Helpers.GetNameFileWithCurrentDate(nameFile);
            DocumentBytes bytes = _documentService.GeneretaNewFiles(new ReportValuesViewModel { ReportTypeId = reportTypeId, Values = values });
            
            switch (driveType)
            {
                case (int)DriveTypeEnum.Private:
                    await _graphServiceFiles.CreateFilesInDrive(Token, nameFile, bytes);

                    break;

                case (int)DriveTypeEnum.Shared:
                    await _graphServiceFiles.CreateFilesInSharedDrive(Token, nameFile, bytes);

                    break;

                default:
                    break;
            }
        }

        #endregion

        [HttpGet("getReportLabels")]
        public ActionResult GetReportLabels(int reportTypeId)
        {
            try
            {
                List<ReportLabel> items = _reportService.GetReportLabels(reportTypeId);
                return Ok(items);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("getReportTypes")]
        public ActionResult GetReportTypes()
        {
            try
            {
                List<ReportType> reportTypes = _reportService.GetReportTypes();
                return Ok(reportTypes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpPost("generateReport")]
        public async Task<ActionResult> GenerateReport([FromBody] GenerateReportModel generateReport)
        {
            try
            {
                await _reportService.GenerateReportAsync(generateReport, UserId, generateReport.Name);

                await PushFilesToDrive(generateReport.Name, generateReport.ReportTypeId, generateReport.Values, (int)DriveTypeEnum.Private);

                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpPost("updateReport")]
        public async Task<ActionResult> UpdateReport([FromBody] UpdateReportModel updateReport)
        {
            try
            {
                await _reportService.UpdateReportAsync(updateReport, UserId);

                await PushFilesToDrive(updateReport.Name, updateReport.ReportTypeId, updateReport.Values, updateReport.DriveType);

                Report report = _reportService.GetReport(updateReport.ReportId);

                return Ok(report);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("getReportHistory")]
        public ActionResult GetReportHistory(int reportId)
        {
            try
            {
                List<ReportHistory> reportHistories = _reportService.GetReportHistory(reportId);

                return Ok(reportHistories);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        
        [HttpGet("getReportHistoryValues")]
        public ActionResult GetReportHistoryValues(int reportHistoryId)
        {
            try
            {
                ReportHistoryViewModel reportHistories = _reportService.GetReportHistoryValues(reportHistoryId);
                return Ok(reportHistories);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("getReports")]
        public ActionResult GetReports()
        {
            try
            {
                List<Report> reports = _reportService.GetReports();

                return Ok(reports);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("getReport")]
        public ActionResult GetReport(int reportId)
        {
            try
            {
                Report report = _reportService.GetReport(reportId);

                return Ok(report);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}