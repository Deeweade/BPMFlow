using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BPMFlow.API.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Action",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Action", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessProcess",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessProcess", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TabNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PositionNum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitNum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitParentNum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FuncManager = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdmManager = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitManager = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsManager = table.Column<bool>(type: "bit", nullable: false),
                    State = table.Column<int>(type: "int", nullable: true),
                    IsStaffMember = table.Column<bool>(type: "bit", nullable: false),
                    HeadOffice = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AmountSubordinate = table.Column<int>(type: "int", nullable: true),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpId = table.Column<int>(type: "int", nullable: true),
                    Parent = table.Column<int>(type: "int", nullable: true),
                    Parents = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Levels = table.Column<int>(type: "int", nullable: true),
                    BlockNum = table.Column<int>(type: "int", nullable: true),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BonusType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntityStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessProcessId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupRequests_BusinessProcess_BusinessProcessId",
                        column: x => x.BusinessProcessId,
                        principalTable: "BusinessProcess",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Role_RoleType_RoleTypeId",
                        column: x => x.RoleTypeId,
                        principalTable: "RoleType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeRole_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RequestStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupRequestId = table.Column<int>(type: "int", nullable: true),
                    ResponsibleRoleId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestStatuses_GroupRequests_GroupRequestId",
                        column: x => x.GroupRequestId,
                        principalTable: "GroupRequests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RequestStatuses_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoleAllowedAction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleAllowedAction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleAllowedAction_Action_ActionId",
                        column: x => x.ActionId,
                        principalTable: "Action",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoleAllowedAction_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AssignedRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResponsibleEmployeeId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    PeriodId = table.Column<int>(type: "int", nullable: true),
                    GroupRequestId = table.Column<int>(type: "int", nullable: true),
                    RequestStatusId = table.Column<int>(type: "int", nullable: true),
                    EntityStatusId = table.Column<int>(type: "int", nullable: true),
                    Code = table.Column<int>(type: "int", nullable: false),
                    DateStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignedRequests_EntityStatus_EntityStatusId",
                        column: x => x.EntityStatusId,
                        principalTable: "EntityStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssignedRequests_GroupRequests_GroupRequestId",
                        column: x => x.GroupRequestId,
                        principalTable: "GroupRequests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssignedRequests_RequestStatuses_RequestStatusId",
                        column: x => x.RequestStatusId,
                        principalTable: "RequestStatuses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RequestStatusesOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusOrder = table.Column<int>(type: "int", nullable: true),
                    IsFinalStatus = table.Column<bool>(type: "bit", nullable: false),
                    GroupRequestId = table.Column<int>(type: "int", nullable: true),
                    RequestStatusId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStatusesOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestStatusesOrders_GroupRequests_GroupRequestId",
                        column: x => x.GroupRequestId,
                        principalTable: "GroupRequests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RequestStatusesOrders_RequestStatuses_RequestStatusId",
                        column: x => x.RequestStatusId,
                        principalTable: "RequestStatuses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RequestStatusTransitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NextStatusId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsNextStageTransition = table.Column<bool>(type: "bit", nullable: false),
                    SkipValidation = table.Column<bool>(type: "bit", nullable: false),
                    GroupRequestId = table.Column<int>(type: "int", nullable: true),
                    SourceStatusId = table.Column<int>(type: "int", nullable: true),
                    RequestStatusId = table.Column<int>(type: "int", nullable: true),
                    ResponsibleRoleId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStatusTransitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestStatusTransitions_GroupRequests_GroupRequestId",
                        column: x => x.GroupRequestId,
                        principalTable: "GroupRequests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RequestStatusTransitions_RequestStatuses_RequestStatusId",
                        column: x => x.RequestStatusId,
                        principalTable: "RequestStatuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RequestStatusTransitions_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignedRequests_EntityStatusId",
                table: "AssignedRequests",
                column: "EntityStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedRequests_GroupRequestId",
                table: "AssignedRequests",
                column: "GroupRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedRequests_RequestStatusId",
                table: "AssignedRequests",
                column: "RequestStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRole_EmployeeId",
                table: "EmployeeRole",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRole_RoleId",
                table: "EmployeeRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupRequests_BusinessProcessId",
                table: "GroupRequests",
                column: "BusinessProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestStatuses_GroupRequestId",
                table: "RequestStatuses",
                column: "GroupRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestStatuses_RoleId",
                table: "RequestStatuses",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestStatusesOrders_GroupRequestId",
                table: "RequestStatusesOrders",
                column: "GroupRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestStatusesOrders_RequestStatusId",
                table: "RequestStatusesOrders",
                column: "RequestStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestStatusTransitions_GroupRequestId",
                table: "RequestStatusTransitions",
                column: "GroupRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestStatusTransitions_RequestStatusId",
                table: "RequestStatusTransitions",
                column: "RequestStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestStatusTransitions_RoleId",
                table: "RequestStatusTransitions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_RoleTypeId",
                table: "Role",
                column: "RoleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleAllowedAction_ActionId",
                table: "RoleAllowedAction",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleAllowedAction_RoleId",
                table: "RoleAllowedAction",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignedRequests");

            migrationBuilder.DropTable(
                name: "EmployeeRole");

            migrationBuilder.DropTable(
                name: "RequestStatusesOrders");

            migrationBuilder.DropTable(
                name: "RequestStatusTransitions");

            migrationBuilder.DropTable(
                name: "RoleAllowedAction");

            migrationBuilder.DropTable(
                name: "EntityStatus");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "RequestStatuses");

            migrationBuilder.DropTable(
                name: "Action");

            migrationBuilder.DropTable(
                name: "GroupRequests");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "BusinessProcess");

            migrationBuilder.DropTable(
                name: "RoleType");
        }
    }
}
