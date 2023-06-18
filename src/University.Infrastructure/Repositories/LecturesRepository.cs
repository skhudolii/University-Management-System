using Microsoft.EntityFrameworkCore;
using University.Core.Entities;
using University.Core.Repositories;
using University.Core.ViewModels.Lecture;
using University.Infrastructure.Data;
using University.Infrastructure.Repositories.Base;

namespace University.Infrastructure.Repositories
{
    public class LecturesRepository : EntityBaseRepository<Lecture>, ILecturesRepository
    {
        private readonly UniversityDbContext _context;

        public LecturesRepository(UniversityDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewLectureAsync(NewLectureVM data)
        {
            var newLecture = new Lecture()
            {
                LectureDate = data.LectureDate,
                StartTime = data.StartTime,
                EndTime = data.EndTime,
                FacultyId = data.FacultyId,
                SubjectId = data.SubjectId,
                LectureRoomId = data.LectureRoomId,
                AcademicEmployeeId = data.AcademicEmployeeId,
            };
            await _context.Lectures.AddAsync(newLecture);
            await _context.SaveChangesAsync();

            // Add Lecture Groups
            foreach (var groupId in data.GroupIds)
            {
                var newLectureGroup = new LectureGroup()
                {
                    LectureId = newLecture.Id,
                    GroupId = groupId
                };
                await _context.LecturesGroups.AddAsync(newLectureGroup);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Lecture> GetLectureByIdAsync(int id)
        {
            var lectureDetails = await _context.Lectures
                .Include(s => s.Subject)
                .Include(l => l.LectureRoom)
                .Include(a => a.Teacher)
                .Include(f => f.Faculty)
                .Include(lg => lg.LecturesGroups).ThenInclude(g => g.Group)
                .FirstOrDefaultAsync(n => n.Id == id);

            return lectureDetails;
        }

        public async Task<NewLectureDropdownsVM> GetNewLectureDropdownsValues()
        {
            var responce = new NewLectureDropdownsVM()
            {
                Faculties = await _context.Faculties.OrderBy(n => n.Name).ToListAsync(),
                Subjects = await _context.Subjects.OrderBy(n => n.Name).ToListAsync(),
                LectureRooms = await _context.LectureRooms.OrderBy(n => n.Name).ToListAsync(),
                Teachers = await _context.AcademicEmployees.OrderBy(n => n.FullName).ToListAsync(),
                Groups = await _context.Groups.OrderBy(n => n.Name).ToListAsync()
            };

            return responce;
        }
    }
}
