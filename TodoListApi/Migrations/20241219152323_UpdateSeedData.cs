using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TodoListApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "Description", "DueDate", "IsCompleted", "Title" },
                values: new object[,]
                {
                    { 1, "Incomplete task 1", new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Initial Task 1" },
                    { 2, "Complete task 2", new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Initial Task 2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
