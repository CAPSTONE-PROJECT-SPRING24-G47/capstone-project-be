using capstone_project_be.Application.Interfaces;
using capstone_project_be.Infrastructure.Context;

namespace capstone_project_be.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProjectContext _dbContext;

        public UnitOfWork(ProjectContext dbContext)
        {
            _dbContext = dbContext;
            UserRepository = new UserRepository(_dbContext);
            VerificationCodeRepository = new VerificationCodeRepository(_dbContext);

            RegionRepository = new RegionRepository(_dbContext);
            PrefectureRepository = new PrefectureRepository(_dbContext);
            CityRepository = new CityRepository(_dbContext);

            AccommodationRepository = new AccommodationRepository(_dbContext);
            AccommodationCategoryRepository = new AccommodationCategoryRepository(_dbContext);
            Acc_AccCategoryRepository = new Acc_AccCategoryRepository(_dbContext);
            AccommodationPhotoRepository = new AccommodationPhotoRepository(_dbContext);
            AccommodationCommentRepository = new AccommodationCommentRepository(_dbContext);

            RestaurantRepository = new RestaurantRepository(_dbContext);
            RestaurantCategoryRepository = new RestaurantCategoryRepository(_dbContext);
            Res_ResCategoryRepository = new Res_ResCategoryRepository(_dbContext);
            RestaurantPhotoRepository = new RestaurantPhotoRepository(_dbContext);
            RestaurantCommentRepository = new RestaurantCommentRepository(_dbContext);

            TouristAttractionRepository = new TouristAttractionRepository(_dbContext);
            TouristAttractionCategoryRepository = new TouristAttractionCategoryRepository(_dbContext);
            TA_TACategoryRepository = new TA_TACategoryRepository(_dbContext);
            TouristAttractionPhotoRepository = new TouristAttractionPhotoRepository(_dbContext);
            TouristAttractionCommentRepository = new TouristAttractionCommentRepository(_dbContext);

            BlogRepository = new BlogRepository(_dbContext);
            BlogCategoryRepository = new BlogCategoryRepository(_dbContext);
            Blog_BlogCategoryRepository = new Blog_BlogCategoryRepository(_dbContext);
            BlogPhotoRepository = new BlogPhotoRepository(_dbContext);
            BlogCommentRepository = new BlogCommentRepository(_dbContext);

            TripRepository = new TripRepository(_dbContext);
            Trip_AccommodationRepository = new Trip_AccommodationRepository(_dbContext);
            Trip_RestaurantRepository = new Trip_RestaurantRepository(_dbContext);
            Trip_TouristAttractionRepository = new Trip_TouristAttractionRepository(_dbContext);
            Trip_LocationRepository = new Trip_LocationRepository(_dbContext);
        }

        public IUserRepository UserRepository { get; set; }
        public IVerificationCodeRepository VerificationCodeRepository { get; set; }

        public IRegionRepository RegionRepository { get; set; }
        public IPrefectureRepository PrefectureRepository { get; set; }
        public ICityRepository CityRepository { get; set; }

        public IAccommodationRepository AccommodationRepository { get; set; }
        public IAccommodationCategoryRepository AccommodationCategoryRepository { get; set; }
        public IAcc_AccCategoryRepository Acc_AccCategoryRepository { get; set; }
        public IAccommodationPhotoRepository AccommodationPhotoRepository { get; set; }
        public IAccommodationCommentRepository AccommodationCommentRepository { get; set; }

        public IRestaurantRepository RestaurantRepository { get; set; }
        public IRestaurantCategoryRepository RestaurantCategoryRepository { get; set; }
        public IRes_ResCategoryRepository Res_ResCategoryRepository { get; set; }
        public IRestaurantPhotoRepository RestaurantPhotoRepository { get; set; }
        public IRestaurantCommentRepository RestaurantCommentRepository { get; set; }

        public ITouristAttractionRepository TouristAttractionRepository { get; set; }
        public ITouristAttractionCategoryRepository TouristAttractionCategoryRepository { get; set; }
        public ITA_TACategoryRepository TA_TACategoryRepository { get; set; }
        public ITouristAttractionPhotoRepository TouristAttractionPhotoRepository { get; set; }
        public ITouristAttractionCommentRepository TouristAttractionCommentRepository { get; set; }

        public IBlogRepository BlogRepository { get; set; }
        public IBlogCategoryRepository BlogCategoryRepository { get; set; }
        public IBlog_BlogCategoryRepository Blog_BlogCategoryRepository { get; set; }
        public IBlogPhotoRepository BlogPhotoRepository { get; set; }
        public IBlogCommentRepository BlogCommentRepository { get; set; }

        public ITripRepository TripRepository { get; set; }
        public ITrip_AccommodationRepository Trip_AccommodationRepository { get; set; }
        public ITrip_RestaurantRepository Trip_RestaurantRepository { get; set; }
        public ITrip_TouristAttractionRepository Trip_TouristAttractionRepository { get; set; }
        public ITrip_LocationRepository Trip_LocationRepository { get; set; }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<int> Save()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
