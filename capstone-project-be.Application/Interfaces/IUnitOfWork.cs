namespace capstone_project_be.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IRegionRepository RegionRepository { get; }
        IPrefectureRepository PrefectureRepository { get; }
        ICityRepository CityRepository { get; }
        IVerificationCodeRepository VerificationCodeRepository { get; }
        IAccommodationRepository AccommodationRepository { get; }
        IRestaurantRepository RestaurantRepository { get; }
        ITouristAttractionRepository TouristAttractionRepository { get; }
        Task<int> Save();
    }
}
