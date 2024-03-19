using capstone_project_be.Application.DTOs.Trip_Accommodations;
using capstone_project_be.Application.DTOs.Trip_Locations;
using capstone_project_be.Application.DTOs.Trip_Restaurants;
using capstone_project_be.Application.DTOs.Trip_TouristAttractions;

namespace capstone_project_be.Application.DTOs.Trips
{
    public class TripDTO
    {
        public required int TripId { get; set; }
        public required int UserId { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }
        public required int Duration { get; set; }
        public required bool IsPublic { get; set; }
        public required DateTime CreatedAt { get; set; }
        public string AccommodationPriceLevel { get; set; }
        public string RestaurantPriceLevel { get; set; }
        public string? AccommodationCategories { get; set; }
        public string? RestaurantCategories { get; set; }
        public string? TouristAttractionCategories { get; set; }

        public IEnumerable<Trip_LocationDTO> Trip_Locations { get; set; }
        public IEnumerable<CRUDTrip_TouristAttractionDTO> Trip_TouristAttractions { get; set; }
        public IEnumerable<CRUDTrip_RestaurantDTO> Trip_Restaurants { get; set; }
        public IEnumerable<CRUDTrip_AccommodationDTO> Trip_Accommodations { get; set; }
    }
}
