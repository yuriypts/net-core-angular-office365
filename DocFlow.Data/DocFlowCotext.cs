using DocFlow.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DocFlow.Data
{
    public class DocFlowCotext : DbContext
    {
        public DocFlowCotext(DbContextOptions<DocFlowCotext> options)
            :base(options)
        { }

        public DbSet<User> Users { get; set; }

        public DbSet<Report> Reports { get; set; }

        public DbSet<ReportType> ReportTypes { get; set; }

        public DbSet<ReportHistory> ReportHistory { get; set; }

        public DbSet<ReportLabel> ReportLabels { get; set; }

        public DbSet<ReportValue> ReportValues { get; set; }
        public DbSet<ReportValuesHistory> ReportValuesHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);       
        }

    }
}