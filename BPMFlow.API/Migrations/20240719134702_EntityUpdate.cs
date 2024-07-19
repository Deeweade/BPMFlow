using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BPMFlow.API.Migrations
{
    /// <inheritdoc />
    public partial class EntityUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "ObjectRequests");

            migrationBuilder.RenameColumn(
                name: "IsNextStageTransition",
                table: "RequestStatusTransitions",
                newName: "IsNextOrderTransition");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsNextOrderTransition",
                table: "RequestStatusTransitions",
                newName: "IsNextStageTransition");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "ObjectRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
