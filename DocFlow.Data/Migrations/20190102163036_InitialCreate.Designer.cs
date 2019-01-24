﻿// <auto-generated />
using System;
using DocFlow.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DocFlow.Data.Migrations
{
    [DbContext(typeof(DocFlowCotext))]
    [Migration("20190102163036_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DocFlow.Data.Entities.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DriveType");

                    b.Property<bool>("IsSigned");

                    b.Property<string>("Name");

                    b.Property<int>("ReportTypeId");

                    b.Property<Guid>("SignerUserId");

                    b.HasKey("Id");

                    b.HasIndex("ReportTypeId");

                    b.HasIndex("SignerUserId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("DocFlow.Data.Entities.ReportHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid>("CreateUserId");

                    b.Property<int>("ReportId");

                    b.HasKey("Id");

                    b.HasIndex("CreateUserId");

                    b.HasIndex("ReportId");

                    b.ToTable("ReportHistory");
                });

            modelBuilder.Entity("DocFlow.Data.Entities.ReportLabel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("ReportTypeId");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("ReportTypeId");

                    b.ToTable("ReportLabels");
                });

            modelBuilder.Entity("DocFlow.Data.Entities.ReportType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<string>("Template");

                    b.HasKey("Id");

                    b.ToTable("ReportTypes");
                });

            modelBuilder.Entity("DocFlow.Data.Entities.ReportValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ReportId");

                    b.Property<int>("ReportLabelId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("ReportId");

                    b.HasIndex("ReportLabelId");

                    b.ToTable("ReportValues");
                });

            modelBuilder.Entity("DocFlow.Data.Entities.ReportValuesHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NewValue");

                    b.Property<string>("OldValue");

                    b.Property<int>("ReportHistoryId");

                    b.Property<int>("ReportValueId");

                    b.HasKey("Id");

                    b.HasIndex("ReportHistoryId");

                    b.ToTable("ReportValuesHistory");
                });

            modelBuilder.Entity("DocFlow.Data.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("UserName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DocFlow.Data.Entities.Report", b =>
                {
                    b.HasOne("DocFlow.Data.Entities.ReportType", "ReportType")
                        .WithMany()
                        .HasForeignKey("ReportTypeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DocFlow.Data.Entities.User", "SignerUser")
                        .WithMany()
                        .HasForeignKey("SignerUserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DocFlow.Data.Entities.ReportHistory", b =>
                {
                    b.HasOne("DocFlow.Data.Entities.User", "CreateUser")
                        .WithMany()
                        .HasForeignKey("CreateUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DocFlow.Data.Entities.Report", "Report")
                        .WithMany()
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DocFlow.Data.Entities.ReportLabel", b =>
                {
                    b.HasOne("DocFlow.Data.Entities.ReportType", "ReportType")
                        .WithMany()
                        .HasForeignKey("ReportTypeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DocFlow.Data.Entities.ReportValue", b =>
                {
                    b.HasOne("DocFlow.Data.Entities.Report")
                        .WithMany("Values")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DocFlow.Data.Entities.ReportLabel", "ReportLabel")
                        .WithMany()
                        .HasForeignKey("ReportLabelId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DocFlow.Data.Entities.ReportValuesHistory", b =>
                {
                    b.HasOne("DocFlow.Data.Entities.ReportHistory")
                        .WithMany("ValuesHistory")
                        .HasForeignKey("ReportHistoryId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
