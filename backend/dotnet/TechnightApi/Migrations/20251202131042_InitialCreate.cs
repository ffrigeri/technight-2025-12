using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TechnightApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "example",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    entry_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_example", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "example",
                columns: new[] { "id", "description", "entry_date", "is_active", "name", "title" },
                values: new object[,]
                {
                    { 1, "This is the first example entry", new DateTime(2025, 12, 2, 13, 10, 41, 875, DateTimeKind.Utc).AddTicks(7880), true, "First Example", "Introduction" },
                    { 2, "This is the second example entry", new DateTime(2025, 12, 1, 13, 10, 41, 875, DateTimeKind.Utc).AddTicks(8230), true, "Second Example", "Advanced Topics" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_example_entry_date",
                table: "example",
                column: "entry_date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "example");
        }
    }
}
