using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace capstone_project_be.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccommodationCategories",
                columns: table => new
                {
                    AccommodationCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccommodationCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccommodationCategories", x => x.AccommodationCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "BlogCategories",
                columns: table => new
                {
                    BlogCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogCategories", x => x.BlogCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    RegionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegionDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.RegionId);
                });

            migrationBuilder.CreateTable(
                name: "RestaurantCategories",
                columns: table => new
                {
                    RestaurantCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestaurantCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantCategories", x => x.RestaurantCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rolename = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "TouristAttractionCategories",
                columns: table => new
                {
                    TouristAttractionCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TouristAttactionCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TouristAttractionCategories", x => x.TouristAttractionCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Prefectures",
                columns: table => new
                {
                    PrefectureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    PrefectureName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrefectureDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prefectures", x => x.PrefectureId);
                    table.ForeignKey(
                        name: "FK_Prefectures_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "RegionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PictureProfile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GoogleToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VerificationCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrefectureId = table.Column<int>(type: "int", nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityId);
                    table.ForeignKey(
                        name: "FK_Cities_Prefectures_PrefectureId",
                        column: x => x.PrefectureId,
                        principalTable: "Prefectures",
                        principalColumn: "PrefectureId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    BlogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlogContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.BlogId);
                    table.ForeignKey(
                        name: "FK_Blogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    TripId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaxBudget = table.Column<float>(type: "real", nullable: false),
                    MinBudget = table.Column<float>(type: "real", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.TripId);
                    table.ForeignKey(
                        name: "FK_Trips_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accommodations",
                columns: table => new
                {
                    AccommodationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    AccommodationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccommodationPrice = table.Column<float>(type: "real", nullable: true),
                    AccommodationAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccommodationWebsite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccommodationPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccommodationDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccommodationLocation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accommodations", x => x.AccommodationId);
                    table.ForeignKey(
                        name: "FK_Accommodations_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    RestaurantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    RestaurantName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RestaurantDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RestaurantPrice = table.Column<float>(type: "real", nullable: true),
                    RestaurantAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RestaurantWebsite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RestaurantPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RestaurantMenu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RestaurantReserveTableUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RestaurantLocation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.RestaurantId);
                    table.ForeignKey(
                        name: "FK_Restaurants_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TouristAttractions",
                columns: table => new
                {
                    TouristAttractionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    TouristAttractionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TouristAttractionPrice = table.Column<float>(type: "real", nullable: true),
                    TouristAttractionAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TouristAttractionWebsite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TouristAttractionDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TouristAttractionLocation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TouristAttractions", x => x.TouristAttractionId);
                    table.ForeignKey(
                        name: "FK_TouristAttractions_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Blog_BlogCatagories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogId = table.Column<int>(type: "int", nullable: false),
                    BlogCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog_BlogCatagories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blog_BlogCatagories_BlogCategories_BlogCategoryId",
                        column: x => x.BlogCategoryId,
                        principalTable: "BlogCategories",
                        principalColumn: "BlogCategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Blog_BlogCatagories_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "BlogId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogComments",
                columns: table => new
                {
                    BlogCommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    BlogId = table.Column<int>(type: "int", nullable: true),
                    Stars = table.Column<float>(type: "real", nullable: false),
                    CommentContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogComments", x => x.BlogCommentId);
                    table.ForeignKey(
                        name: "FK_BlogComments_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "BlogId");
                    table.ForeignKey(
                        name: "FK_BlogComments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "BlogPhotos",
                columns: table => new
                {
                    BlogPhotoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogId = table.Column<int>(type: "int", nullable: false),
                    PhotoURL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPhotos", x => x.BlogPhotoId);
                    table.ForeignKey(
                        name: "FK_BlogPhotos_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "BlogId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trip_Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TripId = table.Column<int>(type: "int", nullable: true),
                    RegionId = table.Column<int>(type: "int", nullable: true),
                    PrefectureId = table.Column<int>(type: "int", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Accommodation_AccommodationCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccomodationId = table.Column<int>(type: "int", nullable: false),
                    AccomodationCategoryId = table.Column<int>(type: "int", nullable: false),
                    AccommodationId = table.Column<int>(type: "int", nullable: false),
                    AccommodationCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accommodation_AccommodationCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accommodation_AccommodationCategories_AccommodationCategories_AccommodationCategoryId",
                        column: x => x.AccommodationCategoryId,
                        principalTable: "AccommodationCategories",
                        principalColumn: "AccommodationCategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accommodation_AccommodationCategories_Accommodations_AccommodationId",
                        column: x => x.AccommodationId,
                        principalTable: "Accommodations",
                        principalColumn: "AccommodationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccommodationComment",
                columns: table => new
                {
                    AccommodationCommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    AccommodationId = table.Column<int>(type: "int", nullable: false),
                    Stars = table.Column<float>(type: "real", nullable: false),
                    CommentContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VisitedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccommodationComment", x => x.AccommodationCommentId);
                    table.ForeignKey(
                        name: "FK_AccommodationComment_Accommodations_AccommodationId",
                        column: x => x.AccommodationId,
                        principalTable: "Accommodations",
                        principalColumn: "AccommodationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccommodationComment_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccommodationPhotos",
                columns: table => new
                {
                    AccommodationPhotoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccommodationId = table.Column<int>(type: "int", nullable: false),
                    PhotoURL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccommodationPhotos", x => x.AccommodationPhotoId);
                    table.ForeignKey(
                        name: "FK_AccommodationPhotos_Accommodations_AccommodationId",
                        column: x => x.AccommodationId,
                        principalTable: "Accommodations",
                        principalColumn: "AccommodationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trip_Accommodations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TripId = table.Column<int>(type: "int", nullable: false),
                    AccommodationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trip_Accommodations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trip_Accommodations_Accommodations_AccommodationId",
                        column: x => x.AccommodationId,
                        principalTable: "Accommodations",
                        principalColumn: "AccommodationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trip_Accommodations_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "TripId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Restaurant_RestaurantCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestaurantId = table.Column<int>(type: "int", nullable: false),
                    RestaurantCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurant_RestaurantCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Restaurant_RestaurantCategories_RestaurantCategories_RestaurantCategoryId",
                        column: x => x.RestaurantCategoryId,
                        principalTable: "RestaurantCategories",
                        principalColumn: "RestaurantCategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Restaurant_RestaurantCategories_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "RestaurantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RestaurantComments",
                columns: table => new
                {
                    RestaurantCommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RestaurantId = table.Column<int>(type: "int", nullable: false),
                    Stars = table.Column<float>(type: "real", nullable: false),
                    CommentContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VisitedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantComments", x => x.RestaurantCommentId);
                    table.ForeignKey(
                        name: "FK_RestaurantComments_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "RestaurantId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RestaurantComments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RestaurantPhotos",
                columns: table => new
                {
                    RestaurantPhotoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestaurantId = table.Column<int>(type: "int", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantPhotos", x => x.RestaurantPhotoId);
                    table.ForeignKey(
                        name: "FK_RestaurantPhotos_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "RestaurantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trip_Restaurants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TripId = table.Column<int>(type: "int", nullable: false),
                    RestaurantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trip_Restaurants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trip_Restaurants_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "RestaurantId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trip_Restaurants_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "TripId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TouristAttraction_TouristAttractionCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TouristAttractionId = table.Column<int>(type: "int", nullable: false),
                    TouristAttractionCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TouristAttraction_TouristAttractionCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TouristAttraction_TouristAttractionCategories_TouristAttractionCategories_TouristAttractionCategoryId",
                        column: x => x.TouristAttractionCategoryId,
                        principalTable: "TouristAttractionCategories",
                        principalColumn: "TouristAttractionCategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TouristAttraction_TouristAttractionCategories_TouristAttractions_TouristAttractionId",
                        column: x => x.TouristAttractionId,
                        principalTable: "TouristAttractions",
                        principalColumn: "TouristAttractionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TouristAttractionComments",
                columns: table => new
                {
                    TouristAttractionCommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TouristAttractionId = table.Column<int>(type: "int", nullable: false),
                    Stars = table.Column<float>(type: "real", nullable: false),
                    CommentContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VisitedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TouristAttractionComments", x => x.TouristAttractionCommentId);
                    table.ForeignKey(
                        name: "FK_TouristAttractionComments_TouristAttractions_TouristAttractionId",
                        column: x => x.TouristAttractionId,
                        principalTable: "TouristAttractions",
                        principalColumn: "TouristAttractionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TouristAttractionComments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TouristAttractionPhotos",
                columns: table => new
                {
                    TouristAttractionPhotoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TouristAttractionId = table.Column<int>(type: "int", nullable: false),
                    PhotoURL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TouristAttractionPhotos", x => x.TouristAttractionPhotoId);
                    table.ForeignKey(
                        name: "FK_TouristAttractionPhotos_TouristAttractions_TouristAttractionId",
                        column: x => x.TouristAttractionId,
                        principalTable: "TouristAttractions",
                        principalColumn: "TouristAttractionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trip_TouristAttractions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TripId = table.Column<int>(type: "int", nullable: false),
                    TouristAttrationId = table.Column<int>(type: "int", nullable: false),
                    TouristAttractionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trip_TouristAttractions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trip_TouristAttractions_TouristAttractions_TouristAttractionId",
                        column: x => x.TouristAttractionId,
                        principalTable: "TouristAttractions",
                        principalColumn: "TouristAttractionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trip_TouristAttractions_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "TripId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accommodation_AccommodationCategories_AccommodationCategoryId",
                table: "Accommodation_AccommodationCategories",
                column: "AccommodationCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Accommodation_AccommodationCategories_AccommodationId",
                table: "Accommodation_AccommodationCategories",
                column: "AccommodationId");

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationComment_AccommodationId",
                table: "AccommodationComment",
                column: "AccommodationId");

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationComment_UserID",
                table: "AccommodationComment",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationPhotos_AccommodationId",
                table: "AccommodationPhotos",
                column: "AccommodationId");

            migrationBuilder.CreateIndex(
                name: "IX_Accommodations_CityId",
                table: "Accommodations",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_BlogCatagories_BlogCategoryId",
                table: "Blog_BlogCatagories",
                column: "BlogCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_BlogCatagories_BlogId",
                table: "Blog_BlogCatagories",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogComments_BlogId",
                table: "BlogComments",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogComments_UserId",
                table: "BlogComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPhotos_BlogId",
                table: "BlogPhotos",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_UserId",
                table: "Blogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_PrefectureId",
                table: "Cities",
                column: "PrefectureId");

            migrationBuilder.CreateIndex(
                name: "IX_Prefectures_RegionId",
                table: "Prefectures",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurant_RestaurantCategories_RestaurantCategoryId",
                table: "Restaurant_RestaurantCategories",
                column: "RestaurantCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurant_RestaurantCategories_RestaurantId",
                table: "Restaurant_RestaurantCategories",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantComments_RestaurantId",
                table: "RestaurantComments",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantComments_UserId",
                table: "RestaurantComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantPhotos_RestaurantId",
                table: "RestaurantPhotos",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_CityId",
                table: "Restaurants",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_TouristAttraction_TouristAttractionCategories_TouristAttractionCategoryId",
                table: "TouristAttraction_TouristAttractionCategories",
                column: "TouristAttractionCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TouristAttraction_TouristAttractionCategories_TouristAttractionId",
                table: "TouristAttraction_TouristAttractionCategories",
                column: "TouristAttractionId");

            migrationBuilder.CreateIndex(
                name: "IX_TouristAttractionComments_TouristAttractionId",
                table: "TouristAttractionComments",
                column: "TouristAttractionId");

            migrationBuilder.CreateIndex(
                name: "IX_TouristAttractionComments_UserId",
                table: "TouristAttractionComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TouristAttractionPhotos_TouristAttractionId",
                table: "TouristAttractionPhotos",
                column: "TouristAttractionId");

            migrationBuilder.CreateIndex(
                name: "IX_TouristAttractions_CityId",
                table: "TouristAttractions",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_Accommodations_AccommodationId",
                table: "Trip_Accommodations",
                column: "AccommodationId");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_Accommodations_TripId",
                table: "Trip_Accommodations",
                column: "TripId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Trip_Restaurants_RestaurantId",
                table: "Trip_Restaurants",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_Restaurants_TripId",
                table: "Trip_Restaurants",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_TouristAttractions_TouristAttractionId",
                table: "Trip_TouristAttractions",
                column: "TouristAttractionId");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_TouristAttractions_TripId",
                table: "Trip_TouristAttractions",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_UserId",
                table: "Trips",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accommodation_AccommodationCategories");

            migrationBuilder.DropTable(
                name: "AccommodationComment");

            migrationBuilder.DropTable(
                name: "AccommodationPhotos");

            migrationBuilder.DropTable(
                name: "Blog_BlogCatagories");

            migrationBuilder.DropTable(
                name: "BlogComments");

            migrationBuilder.DropTable(
                name: "BlogPhotos");

            migrationBuilder.DropTable(
                name: "Restaurant_RestaurantCategories");

            migrationBuilder.DropTable(
                name: "RestaurantComments");

            migrationBuilder.DropTable(
                name: "RestaurantPhotos");

            migrationBuilder.DropTable(
                name: "TouristAttraction_TouristAttractionCategories");

            migrationBuilder.DropTable(
                name: "TouristAttractionComments");

            migrationBuilder.DropTable(
                name: "TouristAttractionPhotos");

            migrationBuilder.DropTable(
                name: "Trip_Accommodations");

            migrationBuilder.DropTable(
                name: "Trip_Locations");

            migrationBuilder.DropTable(
                name: "Trip_Restaurants");

            migrationBuilder.DropTable(
                name: "Trip_TouristAttractions");

            migrationBuilder.DropTable(
                name: "AccommodationCategories");

            migrationBuilder.DropTable(
                name: "BlogCategories");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "RestaurantCategories");

            migrationBuilder.DropTable(
                name: "TouristAttractionCategories");

            migrationBuilder.DropTable(
                name: "Accommodations");

            migrationBuilder.DropTable(
                name: "Restaurants");

            migrationBuilder.DropTable(
                name: "TouristAttractions");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Prefectures");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
