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
            await AddLectureGroupsAsync(newLecture.Id, model);
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

        public async Task UpdateLectureAsync(NewLectureVM model)
        {
            var dbLecture = await _context.Lectures.FirstOrDefaultAsync(n => n.Id == model.Id);

            if (dbLecture != null)
            {
                dbLecture.LectureDate = model.LectureDate;
                dbLecture.StartTime = model.StartTime;
                dbLecture.EndTime = model.EndTime;
                dbLecture.FacultyId = model.FacultyId;
                dbLecture.SubjectId = (int)model.SubjectId;
                dbLecture.LectureRoomId = (int)model.LectureRoomId;
                dbLecture.AcademicEmployeeId = (int)model.AcademicEmployeeId;

                await _context.SaveChangesAsync();
            }

            // Remove existing groups
            var existingGroupsDb = _context.LecturesGroups.Where(n => n.LectureId == model.Id).ToList();
            _context.LecturesGroups.RemoveRange(existingGroupsDb);
            await _context.SaveChangesAsync();

            // Add Lecture Groups
            await AddLectureGroupsAsync(model.Id, model);
        }

        private async Task AddLectureGroupsAsync(int id, NewLectureVM model)
        {
            foreach (var groupId in model.GroupIds)
            {
                var newLectureGroup = new LectureGroup()
                {
                    LectureId = id,
                    GroupId = groupId
                };
                await _context.LecturesGroups.AddAsync(newLectureGroup);
            }
            await _context.SaveChangesAsync();
        }
    }
}
