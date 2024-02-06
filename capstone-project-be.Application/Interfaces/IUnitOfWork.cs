namespace capstone_project_be.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IVerificationCodeRepository VerificationCodeRepository { get; }
        Task<int> Save();
    }
}
