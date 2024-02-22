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
            RestaurantRepository = new RestaurantRepository(_dbContext);
            TouristAttractionRepository = new TouristAttractionRepository(_dbContext);
        }

        public IUserRepository UserRepository { get; set; }
        public IVerificationCodeRepository VerificationCodeRepository { get; set; }
        public IRegionRepository RegionRepository { get; set; }
        public IPrefectureRepository PrefectureRepository { get; set; }
        public ICityRepository CityRepository { get; set; }
        public IAccommodationRepository AccommodationRepository { get; set; }
        public IRestaurantRepository RestaurantRepository { get; set; }
        public ITouristAttractionRepository TouristAttractionRepository { get; set; }

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
