using Microsoft.EntityFrameworkCore;
using University.Core.Entities;
using University.Core.Repositories;
using University.Core.ViewModels.StudentVM;
using University.Infrastructure.Data;
using University.Infrastructure.Repositories.Base;

namespace University.Infrastructure.Repositories
{
    public class StudentsRepository : EntityBaseRepository<Student>, IStudentsRepository
    {
        private readonly UniversityDbContext _context;

        public StudentsRepository(UniversityDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<NewStudentDropdownsVM> GetNewStudentDropdownsValuesAsync()
        {
            var studentDropdowns = new NewStudentDropdownsVM();
            studentDropdowns.Groups = await _context.Groups.OrderBy(n => n.Name).ToListAsync();

            return studentDropdowns;
        }
    }
}
