using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace capstone_project_be.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB040320244 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                table: "Blogs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedAt",
                table: "Blogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
