using System;
using System.ComponentModel.DataAnnotations;

namespace DocFlow.Data.Entities
{
    public class ReportType
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Template { get; set; }

    }
}
