using System.Security.Cryptography.X509Certificates;

namespace capstone_project_be.Domain.Entities
{
    public class TouristAttraction
    {
        public required int TouristAttractionId { get; set; }
        public required int CityId { get; set; }
        public required string TouristAttractionName { get; set;}
        public float? TouristAttractionPrice { get; set;}
        public required string TouristAttractionAddress { get; set;}
        public required string TouristAttractionWebsite { get; set;}
        public required string TouristAttractionDescription { get; set;}
        public required string TouristAttractionLocation { get; set;}
        public required DateTime CreatedAt { get; set; }
        public int? UserId { get; set; }
        public required string Status { get; set; }

        public City City { get; set; }
        public User User { get; set; }

        public IEnumerable<TouristAttractionPhoto> TouristAttractionPhotos { get; set; }
        public IEnumerable<TouristAttractionComment> TouristAttractionComments { get; set; }
        public IEnumerable<TouristAttraction_TouristAttractionCategory>
            TouristAttraction_TouristAttractionCategories { get; set; }
        public IEnumerable<Trip_TouristAttraction> Trip_TouristAttractions { get; set; }
    }
}
