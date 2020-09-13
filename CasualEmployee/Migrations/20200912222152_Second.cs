using Microsoft.EntityFrameworkCore.Migrations;

namespace CasualEmployee.Api.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Task_Assignments");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Tasks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Tasks");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Task_Assignments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
