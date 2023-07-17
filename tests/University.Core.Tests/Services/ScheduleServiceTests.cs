using Moq;
using System.Linq.Expressions;
using University.Core.Entities;
using University.Core.Enums;
using University.Core.Repositories;
using University.Core.Services;

namespace University.Core.Tests.Services
{
    public class ScheduleServiceTests
    {
        private readonly Mock<ILecturesRepository> _mockLecturesRepository;

        public ScheduleServiceTests()
        {
            _mockLecturesRepository = new Mock<ILecturesRepository>();
        }

        [Fact]
        public async Task GetScheduleForFaculty_ReturnScheduleForFaculty()
        {
            // Arrange
            int facultyId = 1;
            var lectures = new List<Lecture>
            {
                new Lecture { Id = 1, FacultyId = 1, LectureDate = DateTime.Now.AddDays(1), StartTime = new TimeSpan(9, 0, 0) },
                new Lecture { Id = 2, FacultyId = 1, LectureDate = DateTime.Now.AddDays(2), StartTime = new TimeSpan(10, 0, 0) },
                new Lecture { Id = 3, FacultyId = 2, LectureDate = DateTime.Now.AddDays(1), StartTime = new TimeSpan(11, 0, 0) },
            };

            _mockLecturesRepository.Setup(repo => repo.GetAllAsync(It.IsAny<Expression<Func<Lecture, object>>[]>()))
                .ReturnsAsync(lectures);

            var scheduleService = new ScheduleService(_mockLecturesRepository.Object);

            // Act
            var result = await scheduleService.GetScheduleForFaculty(facultyId);

            // Assert
            Assert.Equal(2, result.Data.Count()); // Only lectures with facultyId = 1 should be returned
            Assert.Equal(StatusCode.OK, result.StatusCode);

            var scheduleList = result.Data.ToList();
            Assert.Equal(1, scheduleList[0].Id); // Check the first lecture's ID
            Assert.Equal(2, scheduleList[1].Id); // Check the second lecture's ID

            // Check if lecture dates and start times are in ascending order
            for (int i = 1; i < scheduleList.Count; i++)
            {
                var previousLecture = scheduleList[i - 1];
                var currentLecture = scheduleList[i];
                Assert.True(previousLecture.LectureDate <= currentLecture.LectureDate);
                Assert.True(previousLecture.StartTime <= currentLecture.StartTime);
            }

            _mockLecturesRepository.Verify(repo => repo.GetAllAsync(It.IsAny<Expression<Func<Lecture, object>>[]>()), Times.Once);
        }

        [Fact]
        public async Task GetScheduleForFaculty_ExceptionOccurs_ReturnInternalServerError()
        {
            // Arrange
            int facultyId = 1;

            _mockLecturesRepository.Setup(repo => repo.GetAllAsync(It.IsAny<Expression<Func<Lecture, object>>[]>()))
                .ThrowsAsync(new Exception("Something went wrong"));

            var scheduleService = new ScheduleService(_mockLecturesRepository.Object);

            // Act
            var result = await scheduleService.GetScheduleForFaculty(facultyId);

            // Assert
            Assert.Equal(StatusCode.InternalServerError, result.StatusCode);
            Assert.Equal("[ScheduleService.GetScheduleForFaculty] : Something went wrong", result.Description);

            _mockLecturesRepository.Verify(repo => repo.GetAllAsync(It.IsAny<Expression<Func<Lecture, object>>[]>()), Times.Once);
        }

        [Fact]
        public async Task GetScheduleForTeacher_ReturnScheduleForTeacher()
        {
            // Arrange
            int teacherId = 1;
            DateTime lastDateOfPeriod = DateTime.Now.AddDays(7);

            var lectures = new List<Lecture> 
            { 
                new Lecture { Id = 1, FacultyId = 1, AcademicEmployeeId = 1, LectureDate = DateTime.Now.AddDays(1), StartTime = new TimeSpan(9, 0, 0) }, 
                new Lecture { Id = 2, FacultyId = 1, AcademicEmployeeId = 1, LectureDate = DateTime.Now.AddDays(2), StartTime = new TimeSpan(10, 0, 0) }, 
                new Lecture { Id = 3, FacultyId = 1, AcademicEmployeeId = 2, LectureDate = DateTime.Now.AddDays(1), StartTime = new TimeSpan(11, 0, 0) },
            };

            _mockLecturesRepository.Setup(repo => repo.GetAllAsync(It.IsAny<Expression<Func<Lecture, object>>[]>()))
                .ReturnsAsync(lectures);

            var scheduleService = new ScheduleService(_mockLecturesRepository.Object);

            // Act
            var result = await scheduleService.GetScheduleForTeacher(teacherId, lastDateOfPeriod);

            // Assert
            Assert.Equal(2, result.Data.Count()); // Only lectures with AcademicEmployeeId = 1 should be returned
            Assert.Equal(StatusCode.OK, result.StatusCode);

            var scheduleList = result.Data.ToList();
            Assert.Equal(1, scheduleList[0].Id); // Check the first lecture's ID
            Assert.Equal(2, scheduleList[1].Id); // Check the second lecture's ID

            // Check if lecture dates and start times are within the specified period
            foreach (var lecture in scheduleList)
            {
                Assert.True(lecture.LectureDate >= DateTime.Now.Date && lecture.LectureDate <= lastDateOfPeriod.Date);
            }

            _mockLecturesRepository.Verify(repo => repo.GetAllAsync(It.IsAny<Expression<Func<Lecture, object>>[]>()), Times.Once);
        }

        [Fact]
        public async Task GetScheduleForTeacher_ReturnNoContent_WhenTeacherHasNoLectures()
        {
            // Arrange
            int teacherId = 1;
            DateTime lastDateOfPeriod = DateTime.Now.AddDays(7);

            // Mock the repository to return an empty collection of lectures for the teacher
            _mockLecturesRepository.Setup(repo => repo.GetAllAsync(It.IsAny<Expression<Func<Lecture, object>>[]>()))
                .ReturnsAsync(Enumerable.Empty<Lecture>());

            var scheduleService = new ScheduleService(_mockLecturesRepository.Object);

            // Act
            var result = await scheduleService.GetScheduleForTeacher(teacherId, lastDateOfPeriod);

            // Assert
            Assert.Null(result.Data);
            Assert.Equal("0 items found", result.Description);
            Assert.Equal(StatusCode.NoContent, result.StatusCode);

            _mockLecturesRepository.Verify(repo => repo.GetAllAsync(It.IsAny<Expression<Func<Lecture, object>>[]>()), Times.Once);
        }

    }
}
