using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace capstone_project_be.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB260320241 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccommodationComment_Accommodations_AccommodationId",
                table: "AccommodationComment");

            migrationBuilder.DropForeignKey(
                name: "FK_AccommodationComment_Users_UserId",
                table: "AccommodationComment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccommodationComment",
                table: "AccommodationComment");

            migrationBuilder.RenameTable(
                name: "AccommodationComment",
                newName: "AccommodationComments");

            migrationBuilder.RenameIndex(
                name: "IX_AccommodationComment_UserId",
                table: "AccommodationComments",
                newName: "IX_AccommodationComments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AccommodationComment_AccommodationId",
                table: "AccommodationComments",
                newName: "IX_AccommodationComments_AccommodationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccommodationComments",
                table: "AccommodationComments",
                column: "AccommodationCommentId");

            migrationBuilder.CreateTable(
                name: "AccommodationCommentPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccommodationCommentId = table.Column<int>(type: "int", nullable: false),
                    PhotoURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SavedFileName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccommodationCommentPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccommodationCommentPhotos_AccommodationComments_AccommodationCommentId",
                        column: x => x.AccommodationCommentId,
                        principalTable: "AccommodationComments",
                        principalColumn: "AccommodationCommentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RestaurantCommentPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestaurantCommentId = table.Column<int>(type: "int", nullable: false),
                    PhotoURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SavedFileName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantCommentPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RestaurantCommentPhotos_RestaurantComments_RestaurantCommentId",
                        column: x => x.RestaurantCommentId,
                        principalTable: "RestaurantComments",
                        principalColumn: "RestaurantCommentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TouristAttractionCommentPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TouristAttractionCommentId = table.Column<int>(type: "int", nullable: false),
                    PhotoURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SavedFileName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TouristAttractionCommentPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TouristAttractionCommentPhotos_TouristAttractionComments_TouristAttractionCommentId",
                        column: x => x.TouristAttractionCommentId,
                        principalTable: "TouristAttractionComments",
                        principalColumn: "TouristAttractionCommentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationCommentPhotos_AccommodationCommentId",
                table: "AccommodationCommentPhotos",
                column: "AccommodationCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantCommentPhotos_RestaurantCommentId",
                table: "RestaurantCommentPhotos",
                column: "RestaurantCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_TouristAttractionCommentPhotos_TouristAttractionCommentId",
                table: "TouristAttractionCommentPhotos",
                column: "TouristAttractionCommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccommodationComments_Accommodations_AccommodationId",
                table: "AccommodationComments",
                column: "AccommodationId",
                principalTable: "Accommodations",
                principalColumn: "AccommodationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccommodationComments_Users_UserId",
                table: "AccommodationComments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccommodationComments_Accommodations_AccommodationId",
                table: "AccommodationComments");

            migrationBuilder.DropForeignKey(
                name: "FK_AccommodationComments_Users_UserId",
                table: "AccommodationComments");

            migrationBuilder.DropTable(
                name: "AccommodationCommentPhotos");

            migrationBuilder.DropTable(
                name: "RestaurantCommentPhotos");

            migrationBuilder.DropTable(
                name: "TouristAttractionCommentPhotos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccommodationComments",
                table: "AccommodationComments");

            migrationBuilder.RenameTable(
                name: "AccommodationComments",
                newName: "AccommodationComment");

            migrationBuilder.RenameIndex(
                name: "IX_AccommodationComments_UserId",
                table: "AccommodationComment",
                newName: "IX_AccommodationComment_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AccommodationComments_AccommodationId",
                table: "AccommodationComment",
                newName: "IX_AccommodationComment_AccommodationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccommodationComment",
                table: "AccommodationComment",
                column: "AccommodationCommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccommodationComment_Accommodations_AccommodationId",
                table: "AccommodationComment",
                column: "AccommodationId",
                principalTable: "Accommodations",
                principalColumn: "AccommodationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccommodationComment_Users_UserId",
                table: "AccommodationComment",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
