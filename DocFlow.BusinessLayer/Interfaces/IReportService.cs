using DocFlow.BusinessLayer.Models.Report;
using DocFlow.BusinessLayer.Models.Report.History;
using DocFlow.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocFlow.BusinessLayer.Interfaces
{
    public interface IReportService
    {
        List<ReportLabel> GetReportLabels(int reportTypeId);
        List<ReportType> GetReportTypes();
        void GenerateReport(GenerateReportModel generateReport, Guid userId, string nameFile);
        Task GenerateReportAsync(GenerateReportModel generateReport, Guid userId, string nameFile);
        List<ReportHistory> GetReportHistory(int reportId);
        List<Report> GetReports();
        Report GetReport(int reportId);
        Task UpdateReportAsync(UpdateReportModel updateReport, Guid userId);
        void UpdateReport(UpdateReportModel updateReport, Guid userId);
        List<ReportValue> GetReportValues(int reportId);
        ReportHistoryViewModel GetReportHistoryValues(int reportHistoryId);
        Task SignedReport(int reportId);
    }
}
