using AutoMapper;
using capstone_project_be.Application.DTOs.Accommodation_AccommodationCategories;
using capstone_project_be.Application.DTOs.AccommodationCategories;
using capstone_project_be.Application.DTOs.AccommodationComments;
using capstone_project_be.Application.DTOs.AccommodationPhotos;
using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.DTOs.Auths;
using capstone_project_be.Application.DTOs.Blog_BlogCategories;
using capstone_project_be.Application.DTOs.BlogCategories;
using capstone_project_be.Application.DTOs.BlogPhotos;
using capstone_project_be.Application.DTOs.Blogs;
using capstone_project_be.Application.DTOs.Cities;
using capstone_project_be.Application.DTOs.Prefectures;
using capstone_project_be.Application.DTOs.Regions;
using capstone_project_be.Application.DTOs.Restaurant_RestaurantCategories;
using capstone_project_be.Application.DTOs.RestaurantCategories;
using capstone_project_be.Application.DTOs.RestaurantPhotos;
using capstone_project_be.Application.DTOs.Restaurants;
using capstone_project_be.Application.DTOs.TouristAttraction_TouristAttractionCategories;
using capstone_project_be.Application.DTOs.TouristAttractionCategories;
using capstone_project_be.Application.DTOs.TouristAttractionPhotos;
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
            CreateMap<CreateAccommodationDTO, Accommodation>().ReverseMap();
            CreateMap<UpdateAccommodationDTO, Accommodation>().ReverseMap();

            //AccommodationCategory
            CreateMap<AccommodationCategoryDTO, AccommodationCategory>().ReverseMap();

            //Accommodation_AccommodationCategory
            CreateMap<Acc_AccCategoryDTO, Accommodation_AccommodationCategory>().ReverseMap();
            CreateMap<CRUDAcc_AccCategoryDTO, Accommodation_AccommodationCategory>().ReverseMap();

            //AccommodationPhoto
            CreateMap<AccommodationPhotoDTO, AccommodationPhoto>().ReverseMap();
            CreateMap<CRUDAccommodationPhotoDTO, AccommodationPhoto>().ReverseMap();

            //AccommodationComment
            CreateMap<AccommodationCommentDTO, AccommodationComment>().ReverseMap();
            CreateMap<CRUDAccommodationCommentDTO, AccommodationComment>().ReverseMap();

            //Restaurant
            CreateMap<RestaurantDTO, Restaurant>().ReverseMap();
            CreateMap<CreateRestaurantDTO, Restaurant>().ReverseMap();
            CreateMap<UpdateRestaurantDTO, Restaurant>().ReverseMap();

            //Restaurant_RestaurantCategory
            CreateMap<Res_ResCategoryDTO, Restaurant_RestaurantCategory>().ReverseMap();
            CreateMap<CRUDRes_ResCategoryDTO, Restaurant_RestaurantCategory>().ReverseMap();

            //RestaurantPhoto
            CreateMap<RestaurantPhotoDTO, RestaurantPhoto>().ReverseMap();
            CreateMap<CRUDRestaurantPhotoDTO, RestaurantPhoto>().ReverseMap();

            //RestaurantCategory
            CreateMap<RestaurantCategoryDTO, RestaurantCategory>().ReverseMap();

            //TouristAttraction
            CreateMap<TouristAttractionDTO, TouristAttraction>().ReverseMap();
            CreateMap<CreateTouristAttractionDTO, TouristAttraction>().ReverseMap();
            CreateMap<UpdateTouristAttractionDTO, TouristAttraction>().ReverseMap();

            //TouristAttraction_TouristAttractionCategory
            CreateMap<TA_TACategoryDTO, TouristAttraction_TouristAttractionCategory>().ReverseMap();
            CreateMap<CRUDTA_TACategoryDTO, TouristAttraction_TouristAttractionCategory>().ReverseMap();

            //TouristAttractionPhoto
            CreateMap<TouristAttractionPhotoDTO, TouristAttractionPhoto>().ReverseMap();
            CreateMap<CRUDTouristAttractionPhotoDTO, TouristAttractionPhoto>().ReverseMap();

            //TouristAttractionCategory
            CreateMap<TouristAttractionCategoryDTO, TouristAttractionCategory>().ReverseMap();

            //Blog
            CreateMap<BlogDTO, Blog>().ReverseMap();
            CreateMap<CreateBlogDTO, Blog>().ReverseMap();
            CreateMap<UpdateBlogDTO, Blog>().ReverseMap();

            //Blog_BlogCategory
            CreateMap<Blog_BlogCategoryDTO, Blog_BlogCategory>().ReverseMap();
            CreateMap<CRUDBlog_BlogCategoryDTO, Blog_BlogCategory>().ReverseMap();

            //BlogPhoto
            CreateMap<BlogPhotoDTO, BlogPhoto>().ReverseMap();
            CreateMap<CRUDBlogPhotoDTO, BlogPhoto>().ReverseMap();

            //BlogCategory
            CreateMap<BlogCategoryDTO, BlogCategory>().ReverseMap();
        }
    }
}
