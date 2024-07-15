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

            modelBuilder.Entity("BPMFlow.Domain.Models.Entities.BPMFlow.AssignedRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("EntityStatusId")
                        .HasColumnType("int");

                    b.Property<int>("GroupRequestId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("PeriodId")
                        .HasColumnType("int");

                    b.Property<int>("RequestStatusId")
                        .HasColumnType("int");

                    b.Property<int>("ResponsibleEmployeeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GroupRequestId");

                    b.HasIndex("RequestStatusId");

                    b.ToTable("AssignedRequests");
                });

            modelBuilder.Entity("BPMFlow.Domain.Models.Entities.BPMFlow.BusinessProcesses", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BusinessProcesses");
                });

            modelBuilder.Entity("BPMFlow.Domain.Models.Entities.BPMFlow.GroupRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BusinessProcessId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BusinessProcessId");

                    b.ToTable("GroupRequests");
                });

            modelBuilder.Entity("BPMFlow.Domain.Models.Entities.BPMFlow.RequestStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GroupRequestId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ResponsibleRoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GroupRequestId");

                    b.ToTable("RequestStatuses");
                });

            modelBuilder.Entity("BPMFlow.Domain.Models.Entities.BPMFlow.RequestStatusTransition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsNextStageTransition")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NextStatusId")
                        .HasColumnType("int");

                    b.Property<int?>("RequestStatusId")
                        .HasColumnType("int");

                    b.Property<int>("ResponsibleRoleId")
                        .HasColumnType("int");

                    b.Property<bool>("SkipValidation")
                        .HasColumnType("bit");

                    b.Property<int>("SourceStatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RequestStatusId");

                    b.ToTable("RequestStatusTransitions");
                });

            modelBuilder.Entity("BPMFlow.Domain.Models.Entities.BPMFlow.RequestStatusesOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsFinalStatus")
                        .HasColumnType("bit");

                    b.Property<int>("RequestStatusId")
                        .HasColumnType("int");

                    b.Property<int>("StatusOrder")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RequestStatusId");

                    b.ToTable("RequestStatusesOrders");
                });

            modelBuilder.Entity("BPMFlow.Domain.Models.Entities.BPMFlow.AssignedRequest", b =>
                {
                    b.HasOne("BPMFlow.Domain.Models.Entities.BPMFlow.GroupRequest", "GroupRequest")
                        .WithMany("AssignedRequests")
                        .HasForeignKey("GroupRequestId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BPMFlow.Domain.Models.Entities.BPMFlow.RequestStatus", "RequestStatus")
                        .WithMany("AssignedRequests")
                        .HasForeignKey("RequestStatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("GroupRequest");

                    b.Navigation("RequestStatus");
                });

            modelBuilder.Entity("BPMFlow.Domain.Models.Entities.BPMFlow.GroupRequest", b =>
                {
                    b.HasOne("BPMFlow.Domain.Models.Entities.BPMFlow.BusinessProcesses", "BusinessProcesses")
                        .WithMany("GroupRequests")
                        .HasForeignKey("BusinessProcessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BusinessProcesses");
                });

            modelBuilder.Entity("BPMFlow.Domain.Models.Entities.BPMFlow.RequestStatus", b =>
                {
                    b.HasOne("BPMFlow.Domain.Models.Entities.BPMFlow.GroupRequest", "GroupRequest")
                        .WithMany("RequestStatuses")
                        .HasForeignKey("GroupRequestId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("GroupRequest");
                });

            modelBuilder.Entity("BPMFlow.Domain.Models.Entities.BPMFlow.RequestStatusTransition", b =>
                {
                    b.HasOne("BPMFlow.Domain.Models.Entities.BPMFlow.RequestStatus", "RequestStatus")
                        .WithMany("RequestStatusTransitions")
                        .HasForeignKey("RequestStatusId");

                    b.Navigation("RequestStatus");
                });

            modelBuilder.Entity("BPMFlow.Domain.Models.Entities.BPMFlow.RequestStatusesOrder", b =>
                {
                    b.HasOne("BPMFlow.Domain.Models.Entities.BPMFlow.RequestStatus", "RequestStatus")
                        .WithMany("RequestStatusesOrders")
                        .HasForeignKey("RequestStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RequestStatus");
                });

            modelBuilder.Entity("BPMFlow.Domain.Models.Entities.BPMFlow.BusinessProcesses", b =>
                {
                    b.Navigation("GroupRequests");
                });

            modelBuilder.Entity("BPMFlow.Domain.Models.Entities.BPMFlow.GroupRequest", b =>
                {
                    b.Navigation("AssignedRequests");

                    b.Navigation("RequestStatuses");
                });

            modelBuilder.Entity("BPMFlow.Domain.Models.Entities.BPMFlow.RequestStatus", b =>
                {
                    b.Navigation("AssignedRequests");

                    b.Navigation("RequestStatusTransitions");

                    b.Navigation("RequestStatusesOrders");
                });
#pragma warning restore 612, 618
        }
    }
}
