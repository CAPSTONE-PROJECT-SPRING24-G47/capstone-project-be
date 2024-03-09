using capstone_project_be.Application.DTOs.Trip_Accommodations;
using capstone_project_be.Application.DTOs.Trip_Locations;
using capstone_project_be.Application.DTOs.Trip_Restaurants;
using capstone_project_be.Application.DTOs.Trip_TouristAttractions;
using capstone_project_be.Domain.Entities;

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
        public required float MaxBudget { get; set; }
        public required float MinBudget { get; set; }
        public required bool IsPublic { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required bool HasChildren { get; set; }

        public int? RegionId { get; set; }
        public int? PrefectureId { get; set; }
        public int? CityId { get; set; }

        public IEnumerable<CRUDTrip_TouristAttractionDTO> Trip_TouristAttractions { get; set; }
        public IEnumerable<CRUDTrip_RestaurantDTO> Trip_Restaurants { get; set; }
        public IEnumerable<CRUDTrip_AccommodationDTO> Trip_Accommodations { get; set; }
    }
}
