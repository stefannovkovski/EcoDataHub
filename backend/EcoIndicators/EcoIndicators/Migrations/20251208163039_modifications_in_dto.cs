using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoIndicators.Migrations
{
    /// <inheritdoc />
    public partial class modifications_in_dto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Stamp",
                table: "CityDailyAverages",
                newName: "stamp");

            migrationBuilder.AlterColumn<string>(
                name: "stamp",
                table: "CityDailyAverages",
                type: "text",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "stamp",
                table: "CityDailyAverages",
                newName: "Stamp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Stamp",
                table: "CityDailyAverages",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
