using University.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace University.Infrastructure.Data
{
    public class UniversityDbContext : DbContext
    {
        public UniversityDbContext(DbContextOptions<UniversityDbContext> options) : base(options) { }

        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<AcademicEmployee> AcademicEmployees { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        // Aggregate
        public DbSet<SubjectAcademicEmployee> SubjectsAcademicEmployees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubjectAcademicEmployee>().HasKey(sa => new
            {
                sa.SubjectId,
                sa.AcademicEmployeeId                
            });
            
            modelBuilder.Entity<SubjectAcademicEmployee>().HasOne(a => a.AcademicEmployee).WithMany(sa =>
                sa.SubjectsAcademicEmployees).HasForeignKey(a => a.AcademicEmployeeId);
            modelBuilder.Entity<SubjectAcademicEmployee>().HasOne(a => a.Subject).WithMany(sa =>
                sa.SubjectsAcademicEmployees).HasForeignKey(a => a.SubjectId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
