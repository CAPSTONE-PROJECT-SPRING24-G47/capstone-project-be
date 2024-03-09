using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace capstone_project_be.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB090320241 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trip_Locations");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Trips",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Trips",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
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

            migrationBuilder.CreateTable(
                name: "Trip_Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    PrefectureId = table.Column<int>(type: "int", nullable: true),
                    RegionId = table.Column<int>(type: "int", nullable: true),
                    TripId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trip_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trip_Locations_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId");
                    table.ForeignKey(
                        name: "FK_Trip_Locations_Prefectures_PrefectureId",
                        column: x => x.PrefectureId,
                        principalTable: "Prefectures",
                        principalColumn: "PrefectureId");
                    table.ForeignKey(
                        name: "FK_Trip_Locations_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "RegionId");
                    table.ForeignKey(
                        name: "FK_Trip_Locations_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "TripId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trip_Locations_CityId",
                table: "Trip_Locations",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_Locations_PrefectureId",
                table: "Trip_Locations",
                column: "PrefectureId");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_Locations_RegionId",
                table: "Trip_Locations",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_Locations_TripId",
                table: "Trip_Locations",
                column: "TripId");
        }
    }
}
