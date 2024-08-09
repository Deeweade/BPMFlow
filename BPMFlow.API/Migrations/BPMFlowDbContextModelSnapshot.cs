﻿// <auto-generated />
using System;
using BPMFlow.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BPMFlow.API.Migrations
{
    [DbContext(typeof(BPMFlowDbContext))]
    partial class BPMFlowDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BPMFlow.Domain.Models.Entities.BPMFlow.BusinessProcess", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("SystemObjectId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SystemObjectId");

                    b.ToTable("BusinessProcesses");
                });

            modelBuilder.Entity("BPMFlow.Domain.Models.Entities.BPMFlow.ObjectRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorEmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("datetime2");

                    b.Property<int>("EntityStatusId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("ObjectId")
                        .HasColumnType("int");

                    b.Property<int>("PeriodId")
                        .HasColumnType("int");

                    b.Property<int?>("RequestId")
                        .HasColumnType("int");

                    b.Property<int?>("RequestStatusId")
                        .HasColumnType("int");

                    b.Property<int>("RequestStatusTransitionId")
                        .HasColumnType("int");

                    b.Property<int>("ResponsibleEmployeeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RequestId");

                    b.HasIndex("RequestStatusId");

                    b.HasIndex("RequestStatusTransitionId");

                    b.ToTable("ObjectRequests");
                });

            modelBuilder.Entity("BPMFlow.Domain.Models.Entities.BPMFlow.Request", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BusinessProcessId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BusinessProcessId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("BPMFlow.Domain.Models.Entities.BPMFlow.RequestStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsFinalApproved")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFinalDenied")
                        .HasColumnType("bit");

                    b.Property<int?>("RequestId")
                        .HasColumnType("int");

                    b.Property<int>("ResponsibleRoleId")
                        .HasColumnType("int");

                    b.Property<int>("StatusOrder")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RequestId");

                    b.ToTable("RequestStatuses");
                });

            modelBuilder.Entity("BPMFlow.Domain.Models.Entities.BPMFlow.RequestStatusTransition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsNextOrderTransition")
                        .HasColumnType("bit");

                    b.Property<int>("NextStatusOrder")
                        .HasColumnType("int");

                    b.Property<int>("RequestId")
                        .HasColumnType("int");

                    b.Property<int>("ResponsibleRoleId")
                        .HasColumnType("int");

                    b.Property<bool>("SkipValidation")
                        .HasColumnType("bit");

                    b.Property<int>("SourceStatusOrder")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RequestId");

                    b.ToTable("RequestStatusTransitions");
                });

            modelBuilder.Entity("BPMFlow.Domain.Models.Entities.BPMFlow.RequestStatusTrigger", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("RequestStatusId")
                        .HasColumnType("int");

                    b.Property<string>("Trigger")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RequestStatusId");

                    b.ToTable("RequestStatusTriggers");
                });

            modelBuilder.Entity("BPMFlow.Domain.Models.Entities.BPMFlow.System", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Systems");
                });

            modelBuilder.Entity("BPMFlow.Domain.Models.Entities.BPMFlow.SystemObject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("SystemId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SystemId");

                    b.ToTable("SystemObjects");
                });

            modelBuilder.Entity("BPMFlow.Domain.Models.Entities.BPMFlow.BusinessProcess", b =>
                {
                    b.HasOne("BPMFlow.Domain.Models.Entities.BPMFlow.SystemObject", "SystemObject")
                        .WithMany("BusinessProcesses")
                        .HasForeignKey("SystemObjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SystemObject");
                });

            modelBuilder.Entity("BPMFlow.Domain.Models.Entities.BPMFlow.ObjectRequest", b =>
                {
                    b.HasOne("BPMFlow.Domain.Models.Entities.BPMFlow.Request", "Request")
                        .WithMany("ObjectRequests")
                        .HasForeignKey("RequestId");

                    b.HasOne("BPMFlow.Domain.Models.Entities.BPMFlow.RequestStatus", "RequestStatus")
                        .WithMany("ObjectRequests")
                        .HasForeignKey("RequestStatusId");

                    b.HasOne("BPMFlow.Domain.Models.Entities.BPMFlow.RequestStatusTransition", "RequestStatusTransition")
                        .WithMany("ObjectRequests")
                        .HasForeignKey("RequestStatusTransitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Request");

                    b.Navigation("RequestStatus");

                    b.Navigation("RequestStatusTransition");
                });

            modelBuilder.Entity("BPMFlow.Domain.Models.Entities.BPMFlow.Request", b =>
                {
                    b.HasOne("BPMFlow.Domain.Models.Entities.BPMFlow.BusinessProcess", "BusinessProcess")
                        .WithMany("Requests")
                        .HasForeignKey("BusinessProcessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BusinessProcess");
                });

            modelBuilder.Entity("BPMFlow.Domain.Models.Entities.BPMFlow.RequestStatus", b =>
                {
                    b.HasOne("BPMFlow.Domain.Models.Entities.BPMFlow.Request", null)
                        .WithMany("RequestStatuses")
                        .HasForeignKey("RequestId");
                });

            modelBuilder.Entity("BPMFlow.Domain.Models.Entities.BPMFlow.RequestStatusTransition", b =>
                {
                    b.HasOne("BPMFlow.Domain.Models.Entities.BPMFlow.Request", "Request")
                        .WithMany("RequestStatusTransitions")
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Request");
                });

            modelBuilder.Entity("BPMFlow.Domain.Models.Entities.BPMFlow.RequestStatusTrigger", b =>
                {
                    b.HasOne("BPMFlow.Domain.Models.Entities.BPMFlow.RequestStatus", "RequestStatus")
                        .WithMany("RequestStatusTriggers")
                        .HasForeignKey("RequestStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RequestStatus");
                });

            modelBuilder.Entity("BPMFlow.Domain.Models.Entities.BPMFlow.SystemObject", b =>
                {
                    b.HasOne("BPMFlow.Domain.Models.Entities.BPMFlow.System", "System")
                        .WithMany("SystemObjects")
                        .HasForeignKey("SystemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("System");
                });

            modelBuilder.Entity("BPMFlow.Domain.Models.Entities.BPMFlow.BusinessProcess", b =>
                {
                    b.Navigation("Requests");
                });

            modelBuilder.Entity("BPMFlow.Domain.Models.Entities.BPMFlow.Request", b =>
                {
                    b.Navigation("ObjectRequests");

                    b.Navigation("RequestStatusTransitions");

                    b.Navigation("RequestStatuses");
                });

            modelBuilder.Entity("BPMFlow.Domain.Models.Entities.BPMFlow.RequestStatus", b =>
                {
                    b.Navigation("ObjectRequests");

                    b.Navigation("RequestStatusTriggers");
                });

            modelBuilder.Entity("BPMFlow.Domain.Models.Entities.BPMFlow.RequestStatusTransition", b =>
                {
                    b.Navigation("ObjectRequests");
                });

            modelBuilder.Entity("BPMFlow.Domain.Models.Entities.BPMFlow.System", b =>
                {
                    b.Navigation("SystemObjects");
                });

            modelBuilder.Entity("BPMFlow.Domain.Models.Entities.BPMFlow.SystemObject", b =>
                {
                    b.Navigation("BusinessProcesses");
                });
#pragma warning restore 612, 618
        }
    }
}
