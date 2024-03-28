using capstone_project_be.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace capstone_project_be.Infrastructure.Context
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {

        }

        public DbSet<Accommodation> Accommodations { get; set; }
        public DbSet<Accommodation_AccommodationCategory> Accommodation_AccommodationCategories { get; set; }
        public DbSet<AccommodationCategory> AccommodationCategories { get; set; }
        public DbSet<AccommodationComment> AccommodationComments { get; set; }
        public DbSet<AccommodationCommentPhoto> AccommodationCommentPhotos { get; set; }
        public DbSet<AccommodationPhoto> AccommodationPhotos { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Blog_BlogCategory> Blog_BlogCategories { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Prefecture> Prefectures { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Restaurant_RestaurantCategory> Restaurant_RestaurantCategories { get; set; }
        public DbSet<RestaurantCategory> RestaurantCategories { get; set; }
        public DbSet<RestaurantComment> RestaurantComments { get; set; }
        public DbSet<RestaurantCommentPhoto> RestaurantCommentPhotos { get; set; }
        public DbSet<RestaurantPhoto> RestaurantPhotos { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<TouristAttraction> TouristAttractions { get; set; }
        public DbSet<TouristAttraction_TouristAttractionCategory> TouristAttraction_TouristAttractionCategories { get; set; }
        public DbSet<TouristAttractionCategory> TouristAttractionCategories { get; set; }
        public DbSet<TouristAttractionComment> TouristAttractionComments { get; set; }
        public DbSet<TouristAttractionCommentPhoto> TouristAttractionCommentPhotos { get; set; }
        public DbSet<TouristAttractionPhoto> TouristAttractionPhotos { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Trip_Accommodation> Trip_Accommodations { get; set; }
        public DbSet<Trip_Restaurant> Trip_Restaurants { get; set; }
        public DbSet<Trip_TouristAttraction> Trip_TouristAttractions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<VerificationCode> VerificationCodes { get; set; }

        
    }
}
