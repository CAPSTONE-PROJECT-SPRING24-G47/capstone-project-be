using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace capstone_project_be.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUser5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoogleToken",
                table: "Users");

            migrationBuilder.AddColumn<bool>(
                name: "IsGoogleAuth",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsGoogleAuth",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "GoogleToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
