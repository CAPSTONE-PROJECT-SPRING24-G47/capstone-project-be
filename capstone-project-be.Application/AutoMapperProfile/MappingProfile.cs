using AutoMapper;
using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.DTOs.Auths;
using capstone_project_be.Application.DTOs.Cities;
using capstone_project_be.Application.DTOs.Prefectures;
using capstone_project_be.Application.DTOs.Regions;
using capstone_project_be.Application.DTOs.Restaurants;
using capstone_project_be.Application.DTOs.TouristAttractions;
using capstone_project_be.Application.DTOs.Users;
using capstone_project_be.Domain.Entities;

namespace capstone_project_be.Application.AutoMapperProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //User
            CreateMap<UserSignUpDTO, User>().ReverseMap();
            CreateMap<UserSignInDTO, User>().ReverseMap();
            CreateMap<ResetPasswordVerificationDTO, User>().ReverseMap();
            CreateMap<ResetPasswordDTO, User>().ReverseMap();
            CreateMap<GoogleAuthDTO, User>().ReverseMap();
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<CRUDUserDTO, User>().ReverseMap();

            //Region
            CreateMap<RegionDTO, Region>().ReverseMap();
            CreateMap<UpdateRegionDTO, Region>().ReverseMap();

            //Prefecture
            CreateMap<PrefectureDTO, Prefecture>().ReverseMap();
            CreateMap<UpdatePrefectureDTO, Prefecture>().ReverseMap();

            //City
            CreateMap<CityDTO, City>().ReverseMap();
            CreateMap<UpdateCityDTO, City>().ReverseMap();

            //Accommodation
            CreateMap<AccommodationDTO, Accommodation>().ReverseMap();
            CreateMap<CRUDAccommodationDTO, Accommodation>().ReverseMap();

            //Restaurant
            CreateMap<RestaurantDTO, Restaurant>().ReverseMap();
            CreateMap<CRUDRestaurantDTO, Restaurant>().ReverseMap();

            //TouristAttraction
            CreateMap<TouristAttractionDTO, TouristAttraction>().ReverseMap();
            CreateMap<CRUDTouristAttractionDTO, TouristAttraction>().ReverseMap();
        }
    }
}
