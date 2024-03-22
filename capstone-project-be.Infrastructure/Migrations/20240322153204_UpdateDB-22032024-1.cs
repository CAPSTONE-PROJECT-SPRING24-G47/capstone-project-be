using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace capstone_project_be.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB220320241 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_BlogCatagories_BlogCategories_BlogCategoryId",
                table: "Blog_BlogCatagories");

            migrationBuilder.DropForeignKey(
                name: "FK_Blog_BlogCatagories_Blogs_BlogId",
                table: "Blog_BlogCatagories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blog_BlogCatagories",
                table: "Blog_BlogCatagories");

            migrationBuilder.RenameTable(
                name: "Blog_BlogCatagories",
                newName: "Blog_BlogCategories");

            migrationBuilder.RenameIndex(
                name: "IX_Blog_BlogCatagories_BlogId",
                table: "Blog_BlogCategories",
                newName: "IX_Blog_BlogCategories_BlogId");

            migrationBuilder.RenameIndex(
                name: "IX_Blog_BlogCatagories_BlogCategoryId",
                table: "Blog_BlogCategories",
                newName: "IX_Blog_BlogCategories_BlogCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blog_BlogCategories",
                table: "Blog_BlogCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_BlogCategories_BlogCategories_BlogCategoryId",
                table: "Blog_BlogCategories",
                column: "BlogCategoryId",
                principalTable: "BlogCategories",
                principalColumn: "BlogCategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_BlogCategories_Blogs_BlogId",
                table: "Blog_BlogCategories",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "BlogId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_BlogCategories_BlogCategories_BlogCategoryId",
                table: "Blog_BlogCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Blog_BlogCategories_Blogs_BlogId",
                table: "Blog_BlogCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blog_BlogCategories",
                table: "Blog_BlogCategories");

            migrationBuilder.RenameTable(
                name: "Blog_BlogCategories",
                newName: "Blog_BlogCatagories");

            migrationBuilder.RenameIndex(
                name: "IX_Blog_BlogCategories_BlogId",
                table: "Blog_BlogCatagories",
                newName: "IX_Blog_BlogCatagories_BlogId");

            migrationBuilder.RenameIndex(
                name: "IX_Blog_BlogCategories_BlogCategoryId",
                table: "Blog_BlogCatagories",
                newName: "IX_Blog_BlogCatagories_BlogCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blog_BlogCatagories",
                table: "Blog_BlogCatagories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_BlogCatagories_BlogCategories_BlogCategoryId",
                table: "Blog_BlogCatagories",
                column: "BlogCategoryId",
                principalTable: "BlogCategories",
                principalColumn: "BlogCategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_BlogCatagories_Blogs_BlogId",
                table: "Blog_BlogCatagories",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "BlogId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
