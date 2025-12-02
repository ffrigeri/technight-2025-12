using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechnightApi.Migrations
{
    /// <inheritdoc />
    public partial class FixSeedDataDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "example",
                keyColumn: "id",
                keyValue: 1,
                column: "entry_date",
                value: new DateTime(2025, 12, 2, 12, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "example",
                keyColumn: "id",
                keyValue: 2,
                column: "entry_date",
                value: new DateTime(2025, 12, 1, 12, 0, 0, 0, DateTimeKind.Utc));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "example",
                keyColumn: "id",
                keyValue: 1,
                column: "entry_date",
                value: new DateTime(2025, 12, 2, 13, 10, 41, 875, DateTimeKind.Utc).AddTicks(7880));

            migrationBuilder.UpdateData(
                table: "example",
                keyColumn: "id",
                keyValue: 2,
                column: "entry_date",
                value: new DateTime(2025, 12, 1, 13, 10, 41, 875, DateTimeKind.Utc).AddTicks(8230));
        }
    }
}
