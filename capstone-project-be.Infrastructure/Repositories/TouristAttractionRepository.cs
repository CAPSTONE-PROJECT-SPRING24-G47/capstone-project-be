﻿using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using capstone_project_be.Infrastructure.Context;

namespace capstone_project_be.Infrastructure.Repositories
{
    public class TouristAttractionRepository : GenericRepository<TouristAttraction>, ITouristAttractionRepository
    {
        private ProjectContext _dbContext;

        public TouristAttractionRepository(ProjectContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
