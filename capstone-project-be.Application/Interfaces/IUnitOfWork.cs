﻿namespace capstone_project_be.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IVerificationCodeRepository VerificationCodeRepository { get; }

        IRegionRepository RegionRepository { get; }
        IPrefectureRepository PrefectureRepository { get; }
        ICityRepository CityRepository { get; }

        
        IAccommodationRepository AccommodationRepository { get; }
        IAccommodationCategoryRepository AccommodationCategoryRepository { get; }
        IAcc_AccCategoryRepository Acc_AccCategoryRepository { get; }
        IAccommodationPhotoRepository AccommodationPhotoRepository { get; }
        IAccommodationCommentRepository AccommodationCommentRepository { get; }
        IAccommodationCommentPhotoRepository AccommodationCommentPhotoRepository { get; }

        IRestaurantRepository RestaurantRepository { get; }
        IRestaurantCategoryRepository RestaurantCategoryRepository { get; }
        IRes_ResCategoryRepository Res_ResCategoryRepository { get; }
        IRestaurantPhotoRepository RestaurantPhotoRepository { get; }
        IRestaurantCommentRepository RestaurantCommentRepository { get; }
        IRestaurantCommentPhotoRepository RestaurantCommentPhotoRepository { get; }

        ITouristAttractionRepository TouristAttractionRepository { get; }
        ITouristAttractionCategoryRepository TouristAttractionCategoryRepository { get; }
        ITA_TACategoryRepository TA_TACategoryRepository { get; }
        ITouristAttractionPhotoRepository TouristAttractionPhotoRepository { get; }
        ITouristAttractionCommentRepository TouristAttractionCommentRepository { get; }
        ITouristAttractionCommentPhotoRepository TouristAttractionCommentPhotoRepository { get; }

        IBlogRepository BlogRepository { get; }
        IBlogCategoryRepository BlogCategoryRepository { get; }
        IBlog_BlogCategoryRepository Blog_BlogCategoryRepository { get; }
        IBlogCommentRepository BlogCommentRepository { get; }

        ITripRepository TripRepository { get; }
        ITrip_AccommodationRepository Trip_AccommodationRepository { get; }
        ITrip_RestaurantRepository Trip_RestaurantRepository { get; }
        ITrip_TouristAttractionRepository Trip_TouristAttractionRepository { get; }
        ITrip_LocationRepository Trip_LocationRepository { get; }

        Task<int> Save();
    }
}
