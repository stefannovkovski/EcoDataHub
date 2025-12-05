using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EcoIndicators.Migrations
{
    /// <inheritdoc />
    public partial class createdWaterService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Water_For_Productions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Total = table.Column<decimal>(type: "numeric", nullable: false),
                    Fresh_water_tech = table.Column<decimal>(type: "numeric", nullable: false),
                    Fresh_drinking = table.Column<decimal>(type: "numeric", nullable: false),
                    Total_recirculation_water = table.Column<decimal>(type: "numeric", nullable: false),
                    Recurculation_fresh_water_added = table.Column<decimal>(type: "numeric", nullable: false),
                    Reused_water_afterPurifying = table.Column<decimal>(type: "numeric", nullable: false),
                    Reused_water_afterCooling = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Water_For_Productions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Water_For_Productions");
        }
    }
}
