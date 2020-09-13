using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CasualEmployee.Api.Migrations
{
    public partial class Third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Time_Sheets_Tasks_TaskId",
                table: "Employee_Time_Sheets");

            migrationBuilder.DropIndex(
                name: "IX_Employee_Time_Sheets_TaskId",
                table: "Employee_Time_Sheets");

            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "Employee_Time_Sheets");

            migrationBuilder.AddColumn<Guid>(
                name: "AssigneeId",
                table: "Employee_Time_Sheets",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Employee_Time_Sheets",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Task_AssignmentId",
                table: "Employee_Time_Sheets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Time_Sheets_Task_AssignmentId",
                table: "Employee_Time_Sheets",
                column: "Task_AssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Time_Sheets_Task_Assignments_Task_AssignmentId",
                table: "Employee_Time_Sheets",
                column: "Task_AssignmentId",
                principalTable: "Task_Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Time_Sheets_Task_Assignments_Task_AssignmentId",
                table: "Employee_Time_Sheets");

            migrationBuilder.DropIndex(
                name: "IX_Employee_Time_Sheets_Task_AssignmentId",
                table: "Employee_Time_Sheets");

            migrationBuilder.DropColumn(
                name: "AssigneeId",
                table: "Employee_Time_Sheets");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Employee_Time_Sheets");

            migrationBuilder.DropColumn(
                name: "Task_AssignmentId",
                table: "Employee_Time_Sheets");

            migrationBuilder.AddColumn<Guid>(
                name: "TaskId",
                table: "Employee_Time_Sheets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Time_Sheets_TaskId",
                table: "Employee_Time_Sheets",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Time_Sheets_Tasks_TaskId",
                table: "Employee_Time_Sheets",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
