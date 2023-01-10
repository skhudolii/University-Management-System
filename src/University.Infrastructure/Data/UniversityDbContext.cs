using University.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace University.Infrastructure.Data
{
    public class UniversityDbContext : DbContext
    {
        public UniversityDbContext(DbContextOptions<UniversityDbContext> options) : base(options)
        { 
        }

        public DbSet<AcademicEmployee> AcademicEmployees { get; set; }
        public DbSet<Faculty> Faculties { get; set; }        
        public DbSet<Group> Groups { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<LectureRoom> LectureRooms { get; set; }        
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        // Aggregate
        public DbSet<SubjectAcademicEmployee> SubjectsAcademicEmployees { get; set; }
        public DbSet<SubjectGroup> SubjectsGroups { get; set; }
        public DbSet<LectureGroup> LecturesGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<AcademicEmployee>(ConfigureAcademicEmployee);
            //modelBuilder.Entity<Faculty>(ConfigureFaculty);
            modelBuilder.Entity<Group>(ConfigureGroup);
            modelBuilder.Entity<Lecture>(ConfigureLecture);
            //modelBuilder.Entity<LectureRoom>(ConfigureLectureRoom);            
            //modelBuilder.Entity<Student>(ConfigureStudent);
            //modelBuilder.Entity<Subject>(ConfigureSubject);

            modelBuilder.Entity<SubjectAcademicEmployee>(ConfigureSubjectAcademicEmployee);
            modelBuilder.Entity<SubjectGroup>(ConfigureSubjectGroup);
            modelBuilder.Entity<LectureGroup>(ConfigureLectureGroup);
        }

        private void ConfigureGroup(EntityTypeBuilder<Group> builder)
        {
            builder
                .HasMany(s => s.Students)
                .WithOne(g => g.Group)
                .HasForeignKey(g => g.GroupId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigureLecture(EntityTypeBuilder<Lecture> builder)
        {
            builder
                .HasOne(t => t.Teacher)
                .WithMany(l => l.Lectures)
                .HasForeignKey(a => a.AcademicEmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
            builder
                .HasOne(lr => lr.LectureRoom)
                .WithMany(l => l.Lectures)
                .HasForeignKey(lr => lr.LectureRoomId)
                .OnDelete(DeleteBehavior.Restrict);
            builder
                .HasOne(s => s.Subject)
                .WithMany(l => l.Lectures)
                .HasForeignKey(s => s.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        // N&N RELATIONSHIP CONFIGRATION
        private void ConfigureSubjectAcademicEmployee(EntityTypeBuilder<SubjectAcademicEmployee> builder)
        {
            builder
                .HasKey(sa => new { sa.SubjectId, sa.AcademicEmployeeId });
            builder
                .HasOne(a => a.AcademicEmployee)
                .WithMany(sa => sa.SubjectsAcademicEmployees)
                .HasForeignKey(a => a.AcademicEmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
            builder
                .HasOne(a => a.Subject)
                .WithMany(sa => sa.SubjectsAcademicEmployees)
                .HasForeignKey(a => a.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigureSubjectGroup(EntityTypeBuilder<SubjectGroup> builder)
        {
            builder
                .HasKey(sa => new { sa.SubjectId, sa.GroupId });
            builder
                .HasOne(a => a.Group)
                .WithMany(sa => sa.SubjectsGroups)
                .HasForeignKey(a => a.GroupId)
                .OnDelete(DeleteBehavior.Restrict);
            builder
                .HasOne(a => a.Subject)
                .WithMany(sa => sa.SubjectsGroups)
                .HasForeignKey(a => a.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigureLectureGroup(EntityTypeBuilder<LectureGroup> builder)
        {
            builder
                .HasKey(sa => new { sa.LectureId, sa.GroupId });
            builder
                .HasOne(a => a.Group)
                .WithMany(sa => sa.LecturesGroups)
                .HasForeignKey(a => a.GroupId)
                .OnDelete(DeleteBehavior.Restrict);
            builder
                .HasOne(a => a.Lecture)
                .WithMany(sa => sa.LecturesGroups)
                .HasForeignKey(a => a.LectureId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
