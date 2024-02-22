﻿namespace capstone_project_be.Application.DTOs.TouristAttractions
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
    }
}
