using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace capstone_project_be.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB030120241 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RestaurantPrice",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "AccommodationPrice",
                table: "Accommodations");

            migrationBuilder.AddColumn<bool>(
                name: "IsChildrenFriendly",
                table: "Restaurants",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PriceLevel",
                table: "Restaurants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PriceRange",
                table: "Restaurants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsChildrenFriendly",
                table: "Accommodations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PriceLevel",
                table: "Accommodations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PriceRange",
                table: "Accommodations",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsChildrenFriendly",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "PriceLevel",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "PriceRange",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "IsChildrenFriendly",
                table: "Accommodations");

            migrationBuilder.DropColumn(
                name: "PriceLevel",
                table: "Accommodations");

            migrationBuilder.DropColumn(
                name: "PriceRange",
                table: "Accommodations");

            migrationBuilder.AddColumn<float>(
                name: "RestaurantPrice",
                table: "Restaurants",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "AccommodationPrice",
                table: "Accommodations",
                type: "real",
                nullable: true);
        }
    }
}
