using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BPMFlow.API.Migrations
{
    /// <inheritdoc />
    public partial class EntitiesAndRelationshipsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ObjectRequests_Requests_RequestId",
                table: "ObjectRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_BusinessProcesses_BusinessProcessId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestStatusTransitions_Requests_RequestId",
                table: "RequestStatusTransitions");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestStatusTriggers_RequestStatuses_RequestStatusId",
                table: "RequestStatusTriggers");

            migrationBuilder.AddColumn<int>(
                name: "SystemId",
                table: "SystemObjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SystemObjects_SystemId",
                table: "SystemObjects",
                column: "SystemId");

            migrationBuilder.CreateIndex(
                name: "IX_ObjectRequests_RequestStatusTransitionId",
                table: "ObjectRequests",
                column: "RequestStatusTransitionId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessProcesses_SystemId",
                table: "BusinessProcesses",
                column: "SystemId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessProcesses_SystemObjectId",
                table: "BusinessProcesses",
                column: "SystemObjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessProcesses_SystemObjects_SystemObjectId",
                table: "BusinessProcesses",
                column: "SystemObjectId",
                principalTable: "SystemObjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessProcesses_Systems_SystemId",
                table: "BusinessProcesses",
                column: "SystemId",
                principalTable: "Systems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ObjectRequests_RequestStatusTransitions_RequestStatusTransitionId",
                table: "ObjectRequests",
                column: "RequestStatusTransitionId",
                principalTable: "RequestStatusTransitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ObjectRequests_Requests_RequestId",
                table: "ObjectRequests",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_BusinessProcesses_BusinessProcessId",
                table: "Requests",
                column: "BusinessProcessId",
                principalTable: "BusinessProcesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestStatusTransitions_Requests_RequestId",
                table: "RequestStatusTransitions",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestStatusTriggers_RequestStatuses_RequestStatusId",
                table: "RequestStatusTriggers",
                column: "RequestStatusId",
                principalTable: "RequestStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemObjects_Systems_SystemId",
                table: "SystemObjects",
                column: "SystemId",
                principalTable: "Systems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessProcesses_SystemObjects_SystemObjectId",
                table: "BusinessProcesses");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessProcesses_Systems_SystemId",
                table: "BusinessProcesses");

            migrationBuilder.DropForeignKey(
                name: "FK_ObjectRequests_RequestStatusTransitions_RequestStatusTransitionId",
                table: "ObjectRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ObjectRequests_Requests_RequestId",
                table: "ObjectRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_BusinessProcesses_BusinessProcessId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestStatusTransitions_Requests_RequestId",
                table: "RequestStatusTransitions");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestStatusTriggers_RequestStatuses_RequestStatusId",
                table: "RequestStatusTriggers");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemObjects_Systems_SystemId",
                table: "SystemObjects");

            migrationBuilder.DropIndex(
                name: "IX_SystemObjects_SystemId",
                table: "SystemObjects");

            migrationBuilder.DropIndex(
                name: "IX_ObjectRequests_RequestStatusTransitionId",
                table: "ObjectRequests");

            migrationBuilder.DropIndex(
                name: "IX_BusinessProcesses_SystemId",
                table: "BusinessProcesses");

            migrationBuilder.DropIndex(
                name: "IX_BusinessProcesses_SystemObjectId",
                table: "BusinessProcesses");

            migrationBuilder.DropColumn(
                name: "SystemId",
                table: "SystemObjects");

            migrationBuilder.AddForeignKey(
                name: "FK_ObjectRequests_Requests_RequestId",
                table: "ObjectRequests",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_BusinessProcesses_BusinessProcessId",
                table: "Requests",
                column: "BusinessProcessId",
                principalTable: "BusinessProcesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestStatusTransitions_Requests_RequestId",
                table: "RequestStatusTransitions",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestStatusTriggers_RequestStatuses_RequestStatusId",
                table: "RequestStatusTriggers",
                column: "RequestStatusId",
                principalTable: "RequestStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
