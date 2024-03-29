﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace capstone_project_be.Application.DTOs.Accommodations
{
    public class CreateAccommodationDTO
    {
        public int CityId { get; set; }
        public string AccommodationName { get; set; }
        public string PriceRange { get; set; }
        public string AccommodationAddress { get; set; }
        public string AccommodationWebsite { get; set; }
        public string AccommodationPhone { get; set; }
        public string AccommodationDescription { get; set; }
        public string AccommodationLocation { get; set; }
        public int UserId { get; set; }

        [NotMapped]
        public IEnumerable<IFormFile> Photos { get; set; }
        [NotMapped]
        public string Acc_AccCategories { get; set; }
    }
}
