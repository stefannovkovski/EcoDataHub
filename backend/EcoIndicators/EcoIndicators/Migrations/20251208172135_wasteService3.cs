using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EcoIndicators.Migrations
{
    /// <inheritdoc />
    public partial class wasteService3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Collected_and_generated_municipal_wastes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Total = table.Column<decimal>(type: "numeric", nullable: false),
                    Vardar = table.Column<decimal>(type: "numeric", nullable: false),
                    East = table.Column<decimal>(type: "numeric", nullable: false),
                    Southwest = table.Column<decimal>(type: "numeric", nullable: false),
                    Southeast = table.Column<decimal>(type: "numeric", nullable: false),
                    Pelagonia = table.Column<decimal>(type: "numeric", nullable: false),
                    Polog = table.Column<decimal>(type: "numeric", nullable: false),
                    Northeast = table.Column<decimal>(type: "numeric", nullable: false),
                    Skopje = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collected_and_generated_municipal_wastes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Collected_and_generated_municipal_wastes");
        }
    }
}
