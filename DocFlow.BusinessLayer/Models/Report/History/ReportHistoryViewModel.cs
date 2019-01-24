using System;
using System.Collections.Generic;
using System.Text;

namespace DocFlow.BusinessLayer.Models.Report.History
{
    public class ReportHistoryViewModel
    {
        public int Id { get; set; }
        public int ReportId { get; set; }
        public string ReportName { get; set; }        

        public DateTime CreateDate { get; set; }

        public List<ReportValuesHistoryViewModel> Values { get; set; }
    }
}
