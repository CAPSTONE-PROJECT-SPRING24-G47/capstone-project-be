using capstone_project_be.Application.DTOs.Accommodation_AccommodationCategories;
using capstone_project_be.Application.DTOs.AccommodationPhotos;

namespace capstone_project_be.Application.DTOs.Trip_Accommodations
{
    public class CRUDTrip_AccommodationDTO
    {
        public int Id { get; set; }
        public required int AccommodationId { get; set; }
        public int CityId { get; set; }
        public string AccommodationName { get; set; }
        public string PriceRange { get; set; }
        public string PriceLevel { get; set; }
        public string AccommodationAddress { get; set; }
        public string AccommodationWebsite { get; set; }
        public string AccommodationPhone { get; set; }
        public string AccommodationDescription { get; set; }
        public string AccommodationLocation { get; set; }
        public required int SuggestedDay { get; set; }


        public string AccommodationPhotos { get; set; }
        public string AccommodationCategories { get; set; }
    }
}
