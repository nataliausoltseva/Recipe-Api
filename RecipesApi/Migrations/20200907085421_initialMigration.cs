using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipesApi.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recipe",
                columns: table => new
                {
                    RecipeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeImageUrl = table.Column<string>(nullable: true),
                    RecipeName = table.Column<string>(nullable: false),
                    RecipeDifficulty = table.Column<string>(nullable: false),
                    RecipyCost = table.Column<string>(nullable: true),
                    RecipeIngredients = table.Column<string>(nullable: false),
                    RecipeDescription = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe", x => x.RecipeId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recipe");
        }
    }
}
