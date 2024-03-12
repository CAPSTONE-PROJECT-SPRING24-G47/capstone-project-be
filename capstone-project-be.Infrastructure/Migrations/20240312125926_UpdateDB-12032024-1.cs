using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace capstone_project_be.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB120320241 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "HasChildren",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "PrefectureId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "IsChildrenFriendly",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "IsChildrenFriendly",
                table: "Accommodations");

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Trips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Trip_Location",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TripId = table.Column<int>(type: "int", nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: true),
                    PrefectureId = table.Column<int>(type: "int", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trip_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trip_Location_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId");
                    table.ForeignKey(
                        name: "FK_Trip_Location_Prefectures_PrefectureId",
                        column: x => x.PrefectureId,
                        principalTable: "Prefectures",
                        principalColumn: "PrefectureId");
                    table.ForeignKey(
                        name: "FK_Trip_Location_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "RegionId");
                    table.ForeignKey(
                        name: "FK_Trip_Location_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "TripId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trip_Location_CityId",
                table: "Trip_Location",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_Location_PrefectureId",
                table: "Trip_Location",
                column: "PrefectureId");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_Location_RegionId",
                table: "Trip_Location",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_Location_TripId",
                table: "Trip_Location",
                column: "TripId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trip_Location");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Trips");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Trips",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasChildren",
                table: "Trips",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PrefectureId",
                table: "Trips",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RegionId",
                table: "Trips",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsChildrenFriendly",
                table: "Restaurants",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsChildrenFriendly",
                table: "Accommodations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
