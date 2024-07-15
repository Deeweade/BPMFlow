using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BPMFlow.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinessProcesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessProcesses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessProcessId = table.Column<int>(type: "int", nullable: false)
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
                name: "RequestStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponsibleRoleId = table.Column<int>(type: "int", nullable: false),
                    GroupRequestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestStatuses_GroupRequests_GroupRequestId",
                        column: x => x.GroupRequestId,
                        principalTable: "GroupRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssignedRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResponsibleEmployeeId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    PeriodId = table.Column<int>(type: "int", nullable: false),
                    EntityStatusId = table.Column<int>(type: "int", nullable: false),
                    GroupRequestId = table.Column<int>(type: "int", nullable: false),
                    RequestStatusId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false),
                    DateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "RequestStatusesOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusOrder = table.Column<int>(type: "int", nullable: false),
                    IsFinalStatus = table.Column<bool>(type: "bit", nullable: false),
                    RequestStatusId = table.Column<int>(type: "int", nullable: false)
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
                name: "RequestStatusTransitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NextStatusId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsNextStageTransition = table.Column<bool>(type: "bit", nullable: false),
                    SkipValidation = table.Column<bool>(type: "bit", nullable: false),
                    SourceStatusId = table.Column<int>(type: "int", nullable: false),
                    RequestStatusId = table.Column<int>(type: "int", nullable: true),
                    ResponsibleRoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStatusTransitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestStatusTransitions_RequestStatuses_RequestStatusId",
                        column: x => x.RequestStatusId,
                        principalTable: "RequestStatuses",
                        principalColumn: "Id");
                });

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
                name: "IX_RequestStatuses_GroupRequestId",
                table: "RequestStatuses",
                column: "GroupRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestStatusesOrders_RequestStatusId",
                table: "RequestStatusesOrders",
                column: "RequestStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestStatusTransitions_RequestStatusId",
                table: "RequestStatusTransitions",
                column: "RequestStatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignedRequests");

            migrationBuilder.DropTable(
                name: "RequestStatusesOrders");

            migrationBuilder.DropTable(
                name: "RequestStatusTransitions");

            migrationBuilder.DropTable(
                name: "RequestStatuses");

            migrationBuilder.DropTable(
                name: "GroupRequests");

            migrationBuilder.DropTable(
                name: "BusinessProcesses");
        }
    }
}
