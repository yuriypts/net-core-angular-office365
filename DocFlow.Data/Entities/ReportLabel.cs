using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocFlow.Data.Entities
{
    public class ReportLabel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("ReportType")]
        public int ReportTypeId { get; set; }
        public virtual ReportType ReportType { get; set; }

        public int Type { get; set; }
    }
}
