using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EcoIndicators.Migrations
{
    /// <inheritdoc />
    public partial class publicWater : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Public_water_supplys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Total = table.Column<decimal>(type: "numeric", nullable: false),
                    Abstracted_water = table.Column<decimal>(type: "numeric", nullable: false),
                    Ground_water = table.Column<decimal>(type: "numeric", nullable: false),
                    Springs = table.Column<decimal>(type: "numeric", nullable: false),
                    Watercourse = table.Column<decimal>(type: "numeric", nullable: false),
                    Reservoir = table.Column<decimal>(type: "numeric", nullable: false),
                    Lake = table.Column<decimal>(type: "numeric", nullable: false),
                    Water_taken_from_other_water_supply_systems = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Public_water_supplys", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Public_water_supplys");
        }
    }
}
