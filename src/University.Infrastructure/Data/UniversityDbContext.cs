using University.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace University.Infrastructure.Data
{
    public class UniversityDbContext : DbContext
    {
        public UniversityDbContext(DbContextOptions<UniversityDbContext> options) : base(options) { }

        public DbSet<Faculty> Faculties { get; set; }
    }
}
