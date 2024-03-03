﻿using capstone_project_be.Domain.Entities;

namespace capstone_project_be.Application.DTOs.TouristAttractions
{
    public class TouristAttractionDTO
    {
        public int TouristAttractionId { get; set; }
        public int CityId { get; set; }
        public string TouristAttractionName { get; set; }
        public float TouristAttractionPrice { get; set; }
        public string TouristAttractionAddress { get; set; }
        public string TouristAttractionWebsite { get; set; }
        public string TouristAttractionDescription { get; set; }
        public string TouristAttractionLocation { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public required string Status { get; set; }

        public IEnumerable<TouristAttractionPhoto> TouristAttractionPhotos { get; set; }
        public IEnumerable<TouristAttraction_TouristAttractionCategory>
            TouristAttraction_TouristAttractionCategories
        { get; set; }
    }
}
