using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoIndicators.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TABLE IF EXISTS \"SectorCO2\";");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SectorCO2",
                columns: table => new
                {
                },
                constraints: table =>
                {
                });
        }
    }
}
