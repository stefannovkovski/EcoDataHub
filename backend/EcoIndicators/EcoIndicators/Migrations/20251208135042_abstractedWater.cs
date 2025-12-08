using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EcoIndicators.Migrations
{
    /// <inheritdoc />
    public partial class abstractedWater : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Water_abstracted_by_business_entitiess",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    WaterSourceType = table.Column<string>(type: "text", nullable: false),
                    Total = table.Column<decimal>(type: "numeric", nullable: true),
                    MiningAndQuarrying = table.Column<decimal>(type: "numeric", nullable: true),
                    ManufacturingIndustry = table.Column<decimal>(type: "numeric", nullable: true),
                    ElectricityGasSupply = table.Column<decimal>(type: "numeric", nullable: true),
                    AgricultureForestryFishing = table.Column<decimal>(type: "numeric", nullable: true),
                    WaterSupplyWasteManagement = table.Column<decimal>(type: "numeric", nullable: true),
                    Construction = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Water_abstracted_by_business_entitiess", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Water_abstracted_by_business_entitiess");
        }
    }
}
