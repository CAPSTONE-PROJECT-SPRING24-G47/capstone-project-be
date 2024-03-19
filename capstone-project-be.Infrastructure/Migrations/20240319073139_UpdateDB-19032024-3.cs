using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace capstone_project_be.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB190320243 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LocationName",
                table: "Trip_Location",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationName",
                table: "Trip_Location");
        }
    }
}
