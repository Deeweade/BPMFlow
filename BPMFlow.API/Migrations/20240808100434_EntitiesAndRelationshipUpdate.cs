using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BPMFlow.API.Migrations
{
    /// <inheritdoc />
    public partial class EntitiesAndRelationshipUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessProcesses_Systems_SystemId",
                table: "BusinessProcesses",
                column: "SystemId",
                principalTable: "Systems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ObjectRequests_RequestStatusTransitions_RequestStatusTransitionId",
                table: "ObjectRequests",
                column: "RequestStatusTransitionId",
                principalTable: "RequestStatusTransitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemObjects_Systems_SystemId",
                table: "SystemObjects",
                column: "SystemId",
                principalTable: "Systems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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
        }
    }
}
