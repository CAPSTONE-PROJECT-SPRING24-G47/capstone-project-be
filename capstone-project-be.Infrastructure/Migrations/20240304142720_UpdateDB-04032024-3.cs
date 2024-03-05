using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace capstone_project_be.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB040320243 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RestaurantReserveTableUrl",
                table: "Restaurants");

            migrationBuilder.AddColumn<bool>(
                name: "IsReported",
                table: "TouristAttractionComments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsReported",
                table: "RestaurantComments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsReported",
                table: "Blogs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsReported",
                table: "BlogComments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsReported",
                table: "AccommodationComment",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReported",
                table: "TouristAttractionComments");

            migrationBuilder.DropColumn(
                name: "IsReported",
                table: "RestaurantComments");

            migrationBuilder.DropColumn(
                name: "IsReported",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "IsReported",
                table: "BlogComments");

            migrationBuilder.DropColumn(
                name: "IsReported",
                table: "AccommodationComment");

            migrationBuilder.AddColumn<string>(
                name: "RestaurantReserveTableUrl",
                table: "Restaurants",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
