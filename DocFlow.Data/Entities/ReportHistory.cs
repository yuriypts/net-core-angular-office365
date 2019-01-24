using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocFlow.Data.Entities
{
    public class ReportHistory
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Report")]
        public int ReportId { get; set; }

        public virtual Report Report { get; set; }


        [ForeignKey("User")]
        public Guid CreateUserId { get; set; }

        public virtual User CreateUser { get; set; }

        public DateTime CreateDate { get; set; }

        public virtual ICollection<ReportValuesHistory> ValuesHistory { get; set; }


    }
}
