using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BPMFlow.API.Migrations
{
    /// <inheritdoc />
    public partial class ObjectRequestEntityUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RequestId",
                table: "ObjectRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ObjectRequests_RequestId",
                table: "ObjectRequests",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_ObjectRequests_Requests_RequestId",
                table: "ObjectRequests",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ObjectRequests_Requests_RequestId",
                table: "ObjectRequests");

            migrationBuilder.DropIndex(
                name: "IX_ObjectRequests_RequestId",
                table: "ObjectRequests");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "ObjectRequests");
        }
    }
}
