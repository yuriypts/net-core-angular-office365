using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DocFlow.Data.Entities
{
    public class ReportValue
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Report")]
        public int ReportId { get; set; }

        [ForeignKey("ReportLabel")]
        public int ReportLabelId { get; set; }
        public virtual ReportLabel ReportLabel { get; set; }

        public string Value { get; set; }
    }
}
