using Microsoft.EntityFrameworkCore;
using University.Core.Entities;
using University.Core.Repositories;
using University.Core.ViewModels.LectureVM;
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

        public async Task AddNewLectureAsync(NewLectureVM model)
        {
            var newLecture = new Lecture()
            {
                LectureDate = model.LectureDate,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                FacultyId = model.FacultyId,
                SubjectId = (int)model.SubjectId,
                LectureRoomId = (int)model.LectureRoomId,
                AcademicEmployeeId = (int)model.AcademicEmployeeId,
            };
            await _context.Lectures.AddAsync(newLecture);
            await _context.SaveChangesAsync();

            // Add Lecture Groups
            foreach (var groupId in model.GroupIds)
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

        public async Task<Lecture> GetLectureWithIncludePropertiesByIdAsync(int id)
        {
            var lectureDetails = await _context.Lectures
                .Include(s => s.Subject)
                .Include(l => l.LectureRoom)
                .Include(a => a.AcademicEmployee)
                .Include(f => f.Faculty)
                .Include(lg => lg.LecturesGroups).ThenInclude(g => g.Group)
                .FirstOrDefaultAsync(n => n.Id == id);

            return lectureDetails;
        }

        //public async Task<NewLectureDropdownsVM> GetNewLectureDropdownsValues()
        //{
        //    var responce = new NewLectureDropdownsVM()
        //    {
        //        Faculties = await _context.Faculties.OrderBy(n => n.Name).ToListAsync(),
        //        Subjects = await _context.Subjects.OrderBy(n => n.Name).ToListAsync(),
        //        LectureRooms = await _context.LectureRooms.OrderBy(n => n.Name).ToListAsync(),
        //        Teachers = await _context.AcademicEmployees.OrderBy(n => n.FullName).ToListAsync(),
        //        Groups = await _context.Groups.OrderBy(n => n.Name).ToListAsync()
        //    };

        //    return responce;
        //}

        //public async Task UpdateLectureAsync(NewLectureVM data)
        //{
        //    var dbLecture = await _context.Lectures.FirstOrDefaultAsync(n => n.Id == data.Id);

        //    if (dbLecture != null)
        //    {
        //        dbLecture.LectureDate = data.LectureDate;
        //        dbLecture.StartTime = data.StartTime;
        //        dbLecture.EndTime = data.EndTime;
        //        dbLecture.FacultyId = data.FacultyId;
        //        dbLecture.SubjectId = data.SubjectId;
        //        dbLecture.LectureRoomId = data.LectureRoomId;
        //        dbLecture.AcademicEmployeeId = data.AcademicEmployeeId;              

        //        await _context.SaveChangesAsync();
        //    }

        //    // Remove existing groups
        //    var existingGroupsDb = _context.LecturesGroups.Where(n => n.LectureId == data.Id).ToList();
        //    _context.LecturesGroups.RemoveRange(existingGroupsDb);
        //    await _context.SaveChangesAsync();

        //    // Add Lecture Groups
        //    foreach (var groupId in data.GroupIds)
        //    {
        //        var newLectureGroup = new LectureGroup()
        //        {
        //            LectureId = data.Id,
        //            GroupId = groupId
        //        };
        //        await _context.LecturesGroups.AddAsync(newLectureGroup);
        //    }
        //    await _context.SaveChangesAsync();
        //}
    }
}
