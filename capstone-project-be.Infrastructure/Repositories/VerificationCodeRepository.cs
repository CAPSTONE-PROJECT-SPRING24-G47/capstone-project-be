using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using capstone_project_be.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace capstone_project_be.Infrastructure.Repositories
{
    internal class VerificationCodeRepository : GenericRepository<VerificationCode>, IVerificationCodeRepository
    {
        private ProjectContext _dbContext;
        public VerificationCodeRepository(ProjectContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
