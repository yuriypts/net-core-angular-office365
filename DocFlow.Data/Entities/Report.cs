using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DocFlow.Data.Entities
{
    public class Report
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("ReportType")]
        public int ReportTypeId { get; set; }

        public virtual ReportType ReportType { get; set; }

        public int DriveType { get; set; }

        public bool IsSigned { get; set; }

        [ForeignKey("User")]
        public Guid SignerUserId { get; set; }

        public virtual User SignerUser { get; set; }

        public virtual ICollection<ReportValue> Values { get; set; }

    }
}
