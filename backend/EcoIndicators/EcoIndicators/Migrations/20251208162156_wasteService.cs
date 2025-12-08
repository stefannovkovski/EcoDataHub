using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EcoIndicators.Migrations
{
    /// <inheritdoc />
    public partial class wasteService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Amount_of_collected_municipal_wastes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Total = table.Column<decimal>(type: "numeric", nullable: false),
                    Paper = table.Column<decimal>(type: "numeric", nullable: false),
                    Glass = table.Column<decimal>(type: "numeric", nullable: false),
                    Plastic = table.Column<decimal>(type: "numeric", nullable: false),
                    Metal_iron_steel_aluminum = table.Column<decimal>(type: "numeric", nullable: false),
                    Organic_waste_food_leaves = table.Column<decimal>(type: "numeric", nullable: false),
                    Textile = table.Column<decimal>(type: "numeric", nullable: false),
                    Rubber = table.Column<decimal>(type: "numeric", nullable: false),
                    Mixed_municipal_waste = table.Column<decimal>(type: "numeric", nullable: false),
                    Other = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amount_of_collected_municipal_wastes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Amount_of_collected_municipal_wastes");
        }
    }
}
