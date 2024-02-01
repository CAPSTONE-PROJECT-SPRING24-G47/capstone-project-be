using capstone_project_be.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace capstone_project_be.Infrastructure.Context
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {

        }

        public DbSet<User> Books { get; set; }
        public DbSet<Trip> Authors { get; set; }
    }
}
