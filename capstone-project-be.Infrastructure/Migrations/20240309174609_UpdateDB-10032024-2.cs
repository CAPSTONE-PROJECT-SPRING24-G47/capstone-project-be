using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace capstone_project_be.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB100320242 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TouristAttrationId",
                table: "Trip_TouristAttractions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TouristAttrationId",
                table: "Trip_TouristAttractions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
