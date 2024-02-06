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
        public DbSet<AccommodationComment> AccommodationComment { get; set; }
        public DbSet<AccommodationPhoto> AccommodationPhotos { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Blog_BlogCatagory> Blog_BlogCatagories { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<BlogPhoto> BlogPhotos { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Prefecture> Prefectures { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Restaurant_RestaurantCategory> Restaurant_RestaurantCategories { get; set; }
        public DbSet<RestaurantCategory> RestaurantCategories { get; set; }
        public DbSet<RestaurantComment> RestaurantComments { get; set; }
        public DbSet<RestaurantPhoto> RestaurantPhotos { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<TouristAttraction> TouristAttractions { get; set; }
        public DbSet<TouristAttraction_TouristAttractionCategory> TouristAttraction_TouristAttractionCategories { get; set; }
        public DbSet<TouristAttractionCategory> TouristAttractionCategories { get; set; }
        public DbSet<TouristAttractionComment> TouristAttractionComments { get; set; }
        public DbSet<TouristAttractionPhoto> TouristAttractionPhotos { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Trip_Accommodation> Trip_Accommodations { get; set; }
        public DbSet<Trip_Location> Trip_Locations { get; set; }
        public DbSet<Trip_Restaurant> Trip_Restaurants { get; set; }
        public DbSet<Trip_TouristAttraction> Trip_TouristAttractions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<VerificationCode> VerificationCodes { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Accommodation_AccommodationCategory>()
        //            .HasKey(acc => new { acc.AccomodationId, acc.AccomodationCategoryId });
        //    modelBuilder.Entity<Accommodation_AccommodationCategory>()
        //            .HasOne(a => a.Accommodation)
        //            .WithMany(acc => acc.Accommodation_AccommodationCategories)
        //            .HasForeignKey(a => a.AccomodationId);
        //    modelBuilder.Entity<Accommodation_AccommodationCategory>()
        //            .HasOne(ac => ac.AccommodationCategory)
        //            .WithMany(acc => acc.Accommodation_AccommodationCategories)
        //            .HasForeignKey(ac => ac.AccomodationCategoryId);

        //    modelBuilder.Entity<Blog_BlogCatagory>()
        //            .HasKey(bbc => new { bbc.BlogId, bbc.BlogCategoryId });
        //    modelBuilder.Entity<Blog_BlogCatagory>()
        //            .HasOne(b => b.Blog)
        //            .WithMany(bbc => bbc.Blog_BlogCatagories)
        //            .HasForeignKey(b => b.BlogId);
        //    modelBuilder.Entity<Blog_BlogCatagory>()
        //            .HasOne(bc => bc.BlogCategory)
        //            .WithMany(bbc => bbc.Blog_BlogCatagories)
        //            .HasForeignKey(bc => bc.BlogCategoryId);

        //    modelBuilder.Entity<Restaurant_RestaurantCategory>()
        //            .HasKey(rrc => new { rrc.RestaurantId, rrc.RestaurantCategoryId });
        //    modelBuilder.Entity<Restaurant_RestaurantCategory>()
        //            .HasOne(r => r.Restaurant)
        //            .WithMany(rrc => rrc.Restaurant_RestaurantCategories)
        //            .HasForeignKey(r => r.RestaurantId);
        //    modelBuilder.Entity<Restaurant_RestaurantCategory>()
        //            .HasOne(rc => rc.RestaurantCategory)
        //            .WithMany(rrc => rrc.Restaurant_RestaurantCategories)
        //            .HasForeignKey(rc => rc.RestaurantCategoryId);

        //    modelBuilder.Entity<TouristAttraction_TouristAttractionCategory>()
        //            .HasKey(ttc => new { ttc.TouristAttractionId, ttc.TouristAttractionCategoryId });
        //    modelBuilder.Entity<TouristAttraction_TouristAttractionCategory>()
        //            .HasOne(tr => tr.TouristAttraction)
        //            .WithMany(ttc => ttc.TouristAttraction_TouristAttractionCategories)
        //            .HasForeignKey(tr => tr.TouristAttractionId);
        //    modelBuilder.Entity<TouristAttraction_TouristAttractionCategory>()
        //            .HasOne(tc => tc.TouristAttactionCategory)
        //            .WithMany(ttc => ttc.TouristAttraction_TouristAttractionCategories)
        //            .HasForeignKey(tc => tc.TouristAttractionCategoryId);

        //    modelBuilder.Entity<Trip_Accommodation>()
        //            .HasKey(ta => new { ta.TripId, ta.AccommodationId });
        //    modelBuilder.Entity<Trip_Accommodation>()
        //            .HasOne(t => t.Trip)
        //            .WithMany(ta => ta.Trip_Accommodations)
        //            .HasForeignKey(t => t.TripId);
        //    modelBuilder.Entity<Trip_Accommodation>()
        //            .HasOne(a => a.Accommodation)
        //            .WithMany(ta => ta.Trip_Accommodations)
        //            .HasForeignKey(a => a.AccommodationId);

        //    modelBuilder.Entity<Trip_Location>()
        //            .HasKey(tl => new { tl.TripId, tl.RegionId, tl.PrefectureId, tl.CityId });
        //    modelBuilder.Entity<Trip_Location>()
        //            .HasOne(t => t.Trip)
        //            .WithMany(tl => tl.Trip_Locations)
        //            .HasForeignKey(t => t.TripId);
        //    modelBuilder.Entity<Trip_Location>()
        //            .HasOne(r => r.Region)
        //            .WithMany(tl => tl.Trip_Locations)
        //            .HasForeignKey(r => r.RegionId);
        //    modelBuilder.Entity<Trip_Location>()
        //            .HasOne(p => p.Prefecture)
        //            .WithMany(tl => tl.Trip_Locations)
        //            .HasForeignKey(p => p.PrefectureId);
        //    modelBuilder.Entity<Trip_Location>()
        //            .HasOne(c => c.City)
        //            .WithMany(tl => tl.Trip_Locations)
        //            .HasForeignKey(c => c.CityId);

        //    modelBuilder.Entity<Trip_Restaurant>()
        //            .HasKey(tr => new { tr.TripId, tr.Restaurant });
        //    modelBuilder.Entity<Trip_Restaurant>()
        //            .HasOne(t => t.Trip)
        //            .WithMany(tr => tr.Trip_Restaurants)
        //            .HasForeignKey(t => t.TripId);
        //    modelBuilder.Entity<Trip_Restaurant>()
        //            .HasOne(r => r.Restaurant)
        //            .WithMany(tr => tr.Trip_Restaurants)
        //            .HasForeignKey(r => r.RestaurantId);

        //    modelBuilder.Entity<Trip_TouristAttraction>()
        //            .HasKey(tta => new { tta.TripId, tta.TouristAttraction });
        //    modelBuilder.Entity<Trip_TouristAttraction>()
        //            .HasOne(t => t.Trip)
        //            .WithMany(tta => tta.Trip_TouristAttractions)
        //            .HasForeignKey(t => t.TripId);
        //    modelBuilder.Entity<Trip_TouristAttraction>()
        //            .HasOne(tr => tr.TouristAttraction)
        //            .WithMany(tta => tta.Trip_TouristAttractions)
        //            .HasForeignKey(tr => tr.TouristAttrationId);
        //}
    }
}
