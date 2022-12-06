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
        public DbSet<AcademicEmployee_Subject> AcademicEmployees_Subjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AcademicEmployee_Subject>().HasKey(aes => new
            {
                aes.AcademicEmployeeId,
                aes.SubjectId
            });
            
            modelBuilder.Entity<AcademicEmployee_Subject>().HasOne(ae => ae.AcademicEmployee).WithMany(aes =>
                aes.AcademicEmployees_Subjects).HasForeignKey(ae => ae.AcademicEmployeeId);
            modelBuilder.Entity<AcademicEmployee_Subject>().HasOne(ae => ae.Subject).WithMany(aes =>
                aes.AcademicEmployees_Subjects).HasForeignKey(ae => ae.SubjectId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
