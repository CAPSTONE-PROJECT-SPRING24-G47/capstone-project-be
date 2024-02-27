using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace capstone_project_be.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "status",
                table: "TouristAttractions",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Restaurants",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Accommodations",
                newName: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "TouristAttractions",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Restaurants",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Accommodations",
                newName: "status");
        }
    }
}
