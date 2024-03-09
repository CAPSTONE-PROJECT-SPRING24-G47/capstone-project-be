using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace capstone_project_be.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB100320241 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TouristAttactionCategoryName",
                table: "TouristAttractionCategories",
                newName: "TouristAttractionCategoryName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TouristAttractionCategoryName",
                table: "TouristAttractionCategories",
                newName: "TouristAttactionCategoryName");
        }
    }
}
