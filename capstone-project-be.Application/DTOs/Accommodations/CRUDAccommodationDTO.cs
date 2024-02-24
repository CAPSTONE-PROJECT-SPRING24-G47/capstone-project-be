﻿namespace capstone_project_be.Application.DTOs.Accommodations
{
    public class CRUDAccommodationDTO
    {
        public int CityId { get; set; }
        public string AccommodationName { get; set; }
        public float AccommodationPrice { get; set; }
        public string AccommodationAddress { get; set; }
        public string AccommodationWebsite { get; set; }
        public string AccommodationPhone { get; set; }
        public string AccommodationDescription { get; set; }
        public string AccommodationLocation { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required int UserId { get; set; }
    }
}
