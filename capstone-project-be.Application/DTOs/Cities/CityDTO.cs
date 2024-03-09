﻿using capstone_project_be.Domain.Entities;

namespace capstone_project_be.Application.DTOs.Cities
{
    public class CityDTO
    {
        public int CityId { get; set; }
        public int PrefectureId { get; set; }
        public string CityName { get; set; }
        public string CityDescription { get; set; }

        public IEnumerable<TouristAttraction> TouristAttractions { get; set; }
        public IEnumerable<Restaurant> Restaurants { get; set; }
        public IEnumerable<Accommodation> Accommodations { get; set; }

    }
}
