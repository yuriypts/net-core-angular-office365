using DocFlow.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocFlow.BusinessLayer.Models.Report.History
{
    public class ReportValuesHistoryViewModel
    {
        public int Id { get; set; }

        public string OldValue { get; set; }
        public string NewValue { get; set; }

        public virtual ReportLabel ReportLabel { get; set; }

    }
}
