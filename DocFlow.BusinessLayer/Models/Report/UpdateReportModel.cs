using DocFlow.BusinessLayer.Models.FlowWordFile;
using System.Collections.Generic;

namespace DocFlow.BusinessLayer.Models.Report
{
    public class UpdateReportModel
    {
        public string Name { get; set; }
        public int DriveType { get; set; }
        public int ReportId { get; set; }
        public int ReportTypeId { get; set; }
        public List<ReportLabelModel> Values { get; set; }
    }
}
