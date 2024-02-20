﻿namespace capstone_project_be.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IRegionRepository RegionRepository { get; }
        IVerificationCodeRepository VerificationCodeRepository { get; }
        Task<int> Save();
    }
}
