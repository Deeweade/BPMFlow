using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BPMFlow.API.Migrations
{
    /// <inheritdoc />
    public partial class ChangeContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignedRequests");

            migrationBuilder.DropTable(
                name: "RequestStatusesOrders");

            migrationBuilder.DropTable(
                name: "GroupRequests");

            migrationBuilder.DropIndex(
                name: "IX_RequestStatusTransitions_RequestStatusId",
                table: "RequestStatusTransitions");

            migrationBuilder.DropIndex(
                name: "IX_RequestStatuses_GroupRequestId",
                table: "RequestStatuses");

            migrationBuilder.DropColumn(
                name: "RequestStatusId",
                table: "RequestStatusTransitions");

            migrationBuilder.RenameColumn(
                name: "SourceStatusId",
                table: "RequestStatusTransitions",
                newName: "SourceStatusOrder");

            migrationBuilder.RenameColumn(
                name: "NextStatusId",
                table: "RequestStatusTransitions",
                newName: "RequestId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "RequestStatusTransitions",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "RequestStatuses",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "GroupRequestId",
                table: "RequestStatuses",
                newName: "StatusOrder");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "BusinessProcesses",
                newName: "Title");

            migrationBuilder.AddColumn<int>(
                name: "NextStatusOrder",
                table: "RequestStatusTransitions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsFinalApproved",
                table: "RequestStatuses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFinalDenied",
                table: "RequestStatuses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RequestId",
                table: "RequestStatuses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SystemId",
                table: "BusinessProcesses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SystemObjectId",
                table: "BusinessProcesses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ObjectRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ObjectId = table.Column<int>(type: "int", nullable: false),
                    AuthorEmployeeId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ResponsibleEmployeeId = table.Column<int>(type: "int", nullable: false),
                    RequestStatusTransitionId = table.Column<int>(type: "int", nullable: false),
                    PeriodId = table.Column<int>(type: "int", nullable: false),
                    EntityStatusId = table.Column<int>(type: "int", nullable: false),
                    RequestStatusId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false),
                    DateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObjectRequests_RequestStatuses_RequestStatusId",
                        column: x => x.RequestStatusId,
                        principalTable: "RequestStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessProcessId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_BusinessProcesses_BusinessProcessId",
                        column: x => x.BusinessProcessId,
                        principalTable: "BusinessProcesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestStatusTriggers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Trigger = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStatusTriggers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestStatusTriggers_RequestStatuses_RequestStatusId",
                        column: x => x.RequestStatusId,
                        principalTable: "RequestStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestStatusTransitions_RequestId",
                table: "RequestStatusTransitions",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestStatuses_RequestId",
                table: "RequestStatuses",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ObjectRequests_RequestStatusId",
                table: "ObjectRequests",
                column: "RequestStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_BusinessProcessId",
                table: "Requests",
                column: "BusinessProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestStatusTriggers_RequestStatusId",
                table: "RequestStatusTriggers",
                column: "RequestStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestStatuses_Requests_RequestId",
                table: "RequestStatuses",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestStatusTransitions_Requests_RequestId",
                table: "RequestStatusTransitions",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestStatuses_Requests_RequestId",
                table: "RequestStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestStatusTransitions_Requests_RequestId",
                table: "RequestStatusTransitions");

            migrationBuilder.DropTable(
                name: "ObjectRequests");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "RequestStatusTriggers");

            migrationBuilder.DropIndex(
                name: "IX_RequestStatusTransitions_RequestId",
                table: "RequestStatusTransitions");

            migrationBuilder.DropIndex(
                name: "IX_RequestStatuses_RequestId",
                table: "RequestStatuses");

            migrationBuilder.DropColumn(
                name: "NextStatusOrder",
                table: "RequestStatusTransitions");

            migrationBuilder.DropColumn(
                name: "IsFinalApproved",
                table: "RequestStatuses");

            migrationBuilder.DropColumn(
                name: "IsFinalDenied",
                table: "RequestStatuses");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "RequestStatuses");

            migrationBuilder.DropColumn(
                name: "SystemId",
                table: "BusinessProcesses");

            migrationBuilder.DropColumn(
                name: "SystemObjectId",
                table: "BusinessProcesses");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "RequestStatusTransitions",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "SourceStatusOrder",
                table: "RequestStatusTransitions",
                newName: "SourceStatusId");

            migrationBuilder.RenameColumn(
                name: "RequestId",
                table: "RequestStatusTransitions",
                newName: "NextStatusId");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "RequestStatuses",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "StatusOrder",
                table: "RequestStatuses",
                newName: "GroupRequestId");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "BusinessProcesses",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "RequestStatusId",
                table: "RequestStatusTransitions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GroupRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessProcessId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupRequests_BusinessProcesses_BusinessProcessId",
                        column: x => x.BusinessProcessId,
                        principalTable: "BusinessProcesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestStatusesOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestStatusId = table.Column<int>(type: "int", nullable: false),
                    IsFinalStatus = table.Column<bool>(type: "bit", nullable: false),
                    StatusOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStatusesOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestStatusesOrders_RequestStatuses_RequestStatusId",
                        column: x => x.RequestStatusId,
                        principalTable: "RequestStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssignedRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupRequestId = table.Column<int>(type: "int", nullable: false),
                    RequestStatusId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    EntityStatusId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PeriodId = table.Column<int>(type: "int", nullable: false),
                    ResponsibleEmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignedRequests_GroupRequests_GroupRequestId",
                        column: x => x.GroupRequestId,
                        principalTable: "GroupRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignedRequests_RequestStatuses_RequestStatusId",
                        column: x => x.RequestStatusId,
                        principalTable: "RequestStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestStatusTransitions_RequestStatusId",
                table: "RequestStatusTransitions",
                column: "RequestStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestStatuses_GroupRequestId",
                table: "RequestStatuses",
                column: "GroupRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedRequests_GroupRequestId",
                table: "AssignedRequests",
                column: "GroupRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedRequests_RequestStatusId",
                table: "AssignedRequests",
                column: "RequestStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupRequests_BusinessProcessId",
                table: "GroupRequests",
                column: "BusinessProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestStatusesOrders_RequestStatusId",
                table: "RequestStatusesOrders",
                column: "RequestStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestStatuses_GroupRequests_GroupRequestId",
                table: "RequestStatuses",
                column: "GroupRequestId",
                principalTable: "GroupRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestStatusTransitions_RequestStatuses_RequestStatusId",
                table: "RequestStatusTransitions",
                column: "RequestStatusId",
                principalTable: "RequestStatuses",
                principalColumn: "Id");
        }
    }
}
