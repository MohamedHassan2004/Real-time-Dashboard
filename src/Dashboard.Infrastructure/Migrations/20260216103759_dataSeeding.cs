using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Dashboard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Agents",
                columns: new[] { "Id", "LastStatusChange", "Name", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 2, 16, 8, 0, 0, 0, DateTimeKind.Utc), "Ahmed Hassan", 0 },
                    { 2, new DateTime(2026, 2, 16, 8, 15, 0, 0, DateTimeKind.Utc), "Sara Mohamed", 0 },
                    { 3, new DateTime(2026, 2, 16, 8, 30, 0, 0, DateTimeKind.Utc), "Omar Ali", 1 },
                    { 4, new DateTime(2026, 2, 16, 8, 45, 0, 0, DateTimeKind.Utc), "Nour Ibrahim", 2 },
                    { 5, new DateTime(2026, 2, 16, 9, 0, 0, 0, DateTimeKind.Utc), "Youssef Khaled", 0 }
                });

            migrationBuilder.InsertData(
                table: "Queues",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Technical Support" },
                    { 2, "Sales" },
                    { 3, "Billing" }
                });

            migrationBuilder.InsertData(
                table: "Calls",
                columns: new[] { "Id", "AgentId", "AnsweredAt", "CreatedAt", "QueueId", "Status" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 2, 16, 8, 6, 0, 0, DateTimeKind.Utc), new DateTime(2026, 2, 16, 8, 5, 0, 0, DateTimeKind.Utc), 1, 1 },
                    { 2, 2, new DateTime(2026, 2, 16, 8, 12, 0, 0, DateTimeKind.Utc), new DateTime(2026, 2, 16, 8, 10, 0, 0, DateTimeKind.Utc), 1, 1 },
                    { 3, 5, new DateTime(2026, 2, 16, 8, 21, 0, 0, DateTimeKind.Utc), new DateTime(2026, 2, 16, 8, 20, 0, 0, DateTimeKind.Utc), 2, 1 },
                    { 4, 1, new DateTime(2026, 2, 16, 8, 27, 0, 0, DateTimeKind.Utc), new DateTime(2026, 2, 16, 8, 25, 0, 0, DateTimeKind.Utc), 3, 1 },
                    { 5, 2, new DateTime(2026, 2, 16, 8, 36, 0, 0, DateTimeKind.Utc), new DateTime(2026, 2, 16, 8, 35, 0, 0, DateTimeKind.Utc), 2, 1 },
                    { 6, null, null, new DateTime(2026, 2, 16, 8, 15, 0, 0, DateTimeKind.Utc), 1, 2 },
                    { 7, null, null, new DateTime(2026, 2, 16, 8, 40, 0, 0, DateTimeKind.Utc), 3, 2 },
                    { 8, null, null, new DateTime(2026, 2, 16, 8, 50, 0, 0, DateTimeKind.Utc), 1, 0 },
                    { 9, null, null, new DateTime(2026, 2, 16, 8, 55, 0, 0, DateTimeKind.Utc), 2, 0 },
                    { 10, null, null, new DateTime(2026, 2, 16, 8, 58, 0, 0, DateTimeKind.Utc), 3, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Agents",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Agents",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Calls",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Calls",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Calls",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Calls",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Calls",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Calls",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Calls",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Calls",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Calls",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Calls",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Agents",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Agents",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Agents",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Queues",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Queues",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Queues",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
