﻿using capstone_project_be.Application.DTOs.Trip_Locations;

namespace capstone_project_be.Application.DTOs.Trips
{
    public class CreateTripDataDTO
    {
        public required int UserId { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }
        public required float MaxBudget { get; set; }
        public required float MinBudget { get; set; }
        public required bool IsPublic { get; set; }
        public string RestaurantPriceLevel { get; set; }
        public string AccommodationPriceLevel { get; set; }

        public IEnumerable<CreateTrip_LocationDTO> Trip_Locations { get; set; }
        public IEnumerable<int> TouristAttractionCategories { get; set; }
        public IEnumerable<int> RestaurantCategories { get; set; }
        public IEnumerable<int> AccommodationCategories { get; set; }

    }
}