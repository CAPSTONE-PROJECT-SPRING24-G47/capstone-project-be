namespace capstone_project_be.Application.Interfaces
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

        IRestaurantRepository RestaurantRepository { get; }
        IRestaurantCategoryRepository RestaurantCategoryRepository { get; }
        IRes_ResCategoryRepository Res_ResCategoryRepository { get; }
        IRestaurantPhotoRepository RestaurantPhotoRepository { get; }
        IRestaurantCommentRepository RestaurantCommentRepository { get; }

        ITouristAttractionRepository TouristAttractionRepository { get; }
        ITouristAttractionCategoryRepository TouristAttractionCategoryRepository { get; }
        ITA_TACategoryRepository TA_TACategoryRepository { get; }
        ITouristAttractionPhotoRepository TouristAttractionPhotoRepository { get; }
        ITouristAttractionCommentRepository TouristAttractionCommentRepository { get; }

        IBlogRepository BlogRepository { get; }
        IBlogCategoryRepository BlogCategoryRepository { get; }
        IBlog_BlogCategoryRepository Blog_BlogCategoryRepository { get; }
        IBlogPhotoRepository BlogPhotoRepository { get; }
        IBlogCommentRepository BlogCommentRepository { get; }

        Task<int> Save();
    }
}
