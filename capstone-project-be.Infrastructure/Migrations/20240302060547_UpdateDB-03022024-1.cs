using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace capstone_project_be.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB030220241 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccomodationCategoryId",
                table: "Accommodation_AccommodationCategories");

            migrationBuilder.DropColumn(
                name: "AccomodationId",
                table: "Accommodation_AccommodationCategories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccomodationCategoryId",
                table: "Accommodation_AccommodationCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccomodationId",
                table: "Accommodation_AccommodationCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
