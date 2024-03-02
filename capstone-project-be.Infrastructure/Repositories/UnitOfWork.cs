﻿using capstone_project_be.Application.Interfaces;
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
            RestaurantRepository = new RestaurantRepository(_dbContext);
            RestaurantCategoryRepository = new RestaurantCategoryRepository(_dbContext);
            Res_ResCategoryRepository = new Res_ResCategoryRepository(_dbContext);
            TouristAttractionRepository = new TouristAttractionRepository(_dbContext);
            TouristAttractionCategoryRepository = new TouristAttractionCategoryRepository(_dbContext);
            TA_TACategoryRepository = new TA_TACategoryRepository(_dbContext);
        }

        public IUserRepository UserRepository { get; set; }
        public IVerificationCodeRepository VerificationCodeRepository { get; set; }
        public IRegionRepository RegionRepository { get; set; }
        public IPrefectureRepository PrefectureRepository { get; set; }
        public ICityRepository CityRepository { get; set; }
        public IAccommodationRepository AccommodationRepository { get; set; }
        public IAccommodationCategoryRepository AccommodationCategoryRepository { get; set; }
        public IAcc_AccCategoryRepository Acc_AccCategoryRepository { get; set; }
        public IRestaurantRepository RestaurantRepository { get; set; }
        public IRestaurantCategoryRepository RestaurantCategoryRepository { get; set; }
        public IRes_ResCategoryRepository Res_ResCategoryRepository { get; set; }
        public ITouristAttractionRepository TouristAttractionRepository { get; set; }
        public ITouristAttractionCategoryRepository TouristAttractionCategoryRepository { get; set; }
        public ITA_TACategoryRepository TA_TACategoryRepository { get; set; }

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
