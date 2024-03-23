using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace capstone_project_be.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB230320241 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SavedFileName",
                table: "TouristAttractionPhotos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SavedFileName",
                table: "RestaurantPhotos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SavedFileName",
                table: "AccommodationPhotos",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SavedFileName",
                table: "TouristAttractionPhotos");

            migrationBuilder.DropColumn(
                name: "SavedFileName",
                table: "RestaurantPhotos");

            migrationBuilder.DropColumn(
                name: "SavedFileName",
                table: "AccommodationPhotos");
        }
    }
}
