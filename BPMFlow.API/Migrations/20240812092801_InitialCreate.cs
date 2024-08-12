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
                name: "Systems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Systems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemObjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SystemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemObjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemObjects_Systems_SystemId",
                        column: x => x.SystemId,
                        principalTable: "Systems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessProcesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SystemObjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessProcesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessProcesses_SystemObjects_SystemObjectId",
                        column: x => x.SystemObjectId,
                        principalTable: "SystemObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "RequestStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponsibleRoleId = table.Column<int>(type: "int", nullable: false),
                    StatusOrder = table.Column<int>(type: "int", nullable: false),
                    IsFinalApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsFinalDenied = table.Column<bool>(type: "bit", nullable: false),
                    RequestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestStatuses_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestStatusTransitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceStatusOrder = table.Column<int>(type: "int", nullable: false),
                    NextStatusOrder = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsNextOrderTransition = table.Column<bool>(type: "bit", nullable: false),
                    ResponsibleRoleId = table.Column<int>(type: "int", nullable: false),
                    SkipValidation = table.Column<bool>(type: "bit", nullable: false),
                    RequestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStatusTransitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestStatusTransitions_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
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

            migrationBuilder.CreateTable(
                name: "ObjectRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ObjectId = table.Column<int>(type: "int", nullable: false),
                    AuthorEmployeeId = table.Column<int>(type: "int", nullable: false),
                    ResponsibleEmployeeId = table.Column<int>(type: "int", nullable: false),
                    PeriodId = table.Column<int>(type: "int", nullable: false),
                    EntityStatusId = table.Column<int>(type: "int", nullable: false),
                    RequestStatusId = table.Column<int>(type: "int", nullable: false),
                    RequestStatusTransitionId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false),
                    DateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObjectRequests_RequestStatusTransitions_RequestStatusTransitionId",
                        column: x => x.RequestStatusTransitionId,
                        principalTable: "RequestStatusTransitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessProcesses_SystemObjectId",
                table: "BusinessProcesses",
                column: "SystemObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ObjectRequests_RequestStatusId",
                table: "ObjectRequests",
                column: "RequestStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ObjectRequests_RequestStatusTransitionId",
                table: "ObjectRequests",
                column: "RequestStatusTransitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_BusinessProcessId",
                table: "Requests",
                column: "BusinessProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestStatuses_RequestId",
                table: "RequestStatuses",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestStatusTransitions_RequestId",
                table: "RequestStatusTransitions",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestStatusTriggers_RequestStatusId",
                table: "RequestStatusTriggers",
                column: "RequestStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemObjects_SystemId",
                table: "SystemObjects",
                column: "SystemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ObjectRequests");

            migrationBuilder.DropTable(
                name: "RequestStatusTriggers");

            migrationBuilder.DropTable(
                name: "RequestStatusTransitions");

            migrationBuilder.DropTable(
                name: "RequestStatuses");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "BusinessProcesses");

            migrationBuilder.DropTable(
                name: "SystemObjects");

            migrationBuilder.DropTable(
                name: "Systems");
        }
    }
}
