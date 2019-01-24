using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DocFlow.Data.Entities
{
    public class ReportValuesHistory
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ReportHistory")]
        public int ReportHistoryId { get; set; }

        [ForeignKey("ReportValue")]
        public int ReportValueId { get; set; }

        public virtual ReportValue ReportValue { get; set; }

        public string OldValue { get; set; }
        public string NewValue { get; set; }

    }
}
