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
        private Mock<ILecturesRepository> CreateMockLecturesRepository(List<Lecture> lectures)
        {
            var mockLecturesRepository = new Mock<ILecturesRepository>();
            mockLecturesRepository
                .Setup(repo => repo.GetAllAsync(
                    It.IsAny<Expression<Func<Lecture, object>>>(),
                    It.IsAny<Expression<Func<Lecture, object>>>(),
                    It.IsAny<Expression<Func<Lecture, object>>>(),
                    It.IsAny<Expression<Func<Lecture, object>>>()
                ))
                .ReturnsAsync(lectures);

            return mockLecturesRepository;
        }

        [Fact]
        public async Task GetScheduleForFaculty_WithDataSorteredByDate_Success()
        {
            // Arrange
            var facultyId = 1;
            var sortOrder = "date_desc";
            var searchString = "";

            // Sample data to be returned by the mock repository
            var lectures = new List<Lecture>
            {
                new Lecture { Id = 1, FacultyId = 1, AcademicEmployeeId = 1, SubjectId = 1, LectureRoomId = 1, LectureDate = DateTime.Now.AddDays(2), StartTime = new TimeSpan(9, 0, 0) },
                new Lecture { Id = 2, FacultyId = 1, AcademicEmployeeId = 1, SubjectId = 2, LectureRoomId = 1, LectureDate = DateTime.Now.AddDays(3), StartTime = new TimeSpan(10, 0, 0) },
                new Lecture { Id = 3, FacultyId = 1, AcademicEmployeeId = 2, SubjectId = 3, LectureRoomId = 1, LectureDate = DateTime.Now.AddDays(1), StartTime = new TimeSpan(11, 0, 0) }
            };

            var mockLecturesRepository = CreateMockLecturesRepository(lectures);

            var scheduleService = new ScheduleService(mockLecturesRepository.Object);

            // Act
            var response = await scheduleService.GetScheduleForFaculty(facultyId, sortOrder, searchString);

            // Sort the lectures list to match the response.Data sorting
            var expectedSorted = lectures.OrderByDescending(l => l.LectureDate);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(StatusCode.OK, response.StatusCode);
            Assert.Equal(expectedSorted, response.Data);
            Assert.True(expectedSorted.SequenceEqual(response.Data));
        }

        [Fact]
        public async Task GetScheduleForFaculty_WithNoData_Success()
        {
            // Arrange
            var facultyId = 2;
            var sortOrder = "date_desc";
            var searchString = "Biology";

            // Sample empty data to be returned by the mock repository
            var lectures = new List<Lecture>();

            var mockLecturesRepository = CreateMockLecturesRepository(lectures);

            var scheduleService = new ScheduleService(mockLecturesRepository.Object);

            // Act
            var response = await scheduleService.GetScheduleForFaculty(facultyId, sortOrder, searchString);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(StatusCode.NoContent, response.StatusCode);
            Assert.Null(response.Data);
        }

        [Fact]
        public async Task GetScheduleForFaculty_Error()
        {
            // Arrange
            var facultyId = 3;
            var sortOrder = "date_desc";
            var searchString = "Chemistry";

            // Sample data that throws an exception in the mock repository
            var lectures = new List<Lecture>();

            var mockLecturesRepository = new Mock<ILecturesRepository>();
            mockLecturesRepository
                .Setup(repo => repo.GetAllAsync(
                    It.IsAny<Expression<Func<Lecture, object>>>(),
                    It.IsAny<Expression<Func<Lecture, object>>>(),
                    It.IsAny<Expression<Func<Lecture, object>>>(),
                    It.IsAny<Expression<Func<Lecture, object>>>()
                ))
                .ThrowsAsync(new Exception("Mock repository error."));

            var scheduleService = new ScheduleService(mockLecturesRepository.Object);

            // Act
            var response = await scheduleService.GetScheduleForFaculty(facultyId, sortOrder, searchString);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(StatusCode.InternalServerError, response.StatusCode);
            Assert.Null(response.Data);
            Assert.Contains("Mock repository error.", response.Description);
        }

        [Fact]
        public async Task GetScheduleForTeacher_WithDataSorteredByDate_Success()
        {
            // Arrange
            var teacherId = 1;
            var lastDateOfPeriod = DateTime.Now.AddDays(7); // Set the last date of the period to 7 days from now
            var sortOrder = "date_desc";
            var searchString = "";

            // Sample data to be returned by the mock repository
            var lectures = new List<Lecture>
            {
                new Lecture { Id = 1, FacultyId = 1, AcademicEmployeeId = 1, SubjectId = 1, LectureRoomId = 1, LectureDate = DateTime.Now.AddDays(2), StartTime = new TimeSpan(9, 0, 0) },
                new Lecture { Id = 2, FacultyId = 1, AcademicEmployeeId = 1, SubjectId = 2, LectureRoomId = 1, LectureDate = DateTime.Now.AddDays(3), StartTime = new TimeSpan(10, 0, 0) },
                new Lecture { Id = 3, FacultyId = 1, AcademicEmployeeId = 1, SubjectId = 3, LectureRoomId = 1, LectureDate = DateTime.Now.AddDays(1), StartTime = new TimeSpan(11, 0, 0) },
                new Lecture { Id = 4, FacultyId = 2, AcademicEmployeeId = 2, SubjectId = 4, LectureRoomId = 2, LectureDate = DateTime.Now.AddDays(5), StartTime = new TimeSpan(13, 0, 0) },
                new Lecture { Id = 5, FacultyId = 2, AcademicEmployeeId = 2, SubjectId = 5, LectureRoomId = 2, LectureDate = DateTime.Now.AddDays(4), StartTime = new TimeSpan(14, 0, 0) },
            };

            var mockLecturesRepository = CreateMockLecturesRepository(lectures);
            var scheduleService = new ScheduleService(mockLecturesRepository.Object);

            // Act
            var response = await scheduleService.GetScheduleForTeacher(teacherId, lastDateOfPeriod, sortOrder, searchString);

            // Sort the lectures list to match the response.Data sorting
            var expectedSorted = lectures
                .Where(l => l.AcademicEmployeeId == teacherId && l.LectureDate >= DateTime.Now.Date && l.LectureDate <= lastDateOfPeriod.Date)
                .OrderByDescending(l => l.LectureDate)
                .ThenBy(l => l.StartTime)
                .ToList();

            // Assert
            Assert.NotNull(response);
            Assert.Equal(StatusCode.OK, response.StatusCode);
            Assert.Equal(expectedSorted, response.Data);
            Assert.True(expectedSorted.SequenceEqual(response.Data));
        }

        [Fact]
        public async Task GetScheduleForTeacher_WithNoLecturesFromToday_NoContent()
        {
            // Arrange
            var teacherId = 2;
            var lastDateOfPeriod = DateTime.Now.AddDays(7); // Set the last date of the period to 7 days from now
            var sortOrder = "date_desc";
            var searchString = "";

            // Sample data to be returned by the mock repository
            var lectures = new List<Lecture>
            {
                new Lecture { Id = 1, FacultyId = 1, AcademicEmployeeId = 2, SubjectId = 1, LectureRoomId = 1, LectureDate = DateTime.Now.AddDays(-2), StartTime = new TimeSpan(9, 0, 0) },
                new Lecture { Id = 2, FacultyId = 1, AcademicEmployeeId = 2, SubjectId = 2, LectureRoomId = 1, LectureDate = DateTime.Now.AddDays(-3), StartTime = new TimeSpan(10, 0, 0) },
                new Lecture { Id = 3, FacultyId = 1, AcademicEmployeeId = 2, SubjectId = 3, LectureRoomId = 1, LectureDate = DateTime.Now.AddDays(-1), StartTime = new TimeSpan(11, 0, 0) },
                new Lecture { Id = 4, FacultyId = 1, AcademicEmployeeId = 2, SubjectId = 4, LectureRoomId = 2, LectureDate = DateTime.Now.AddDays(-5), StartTime = new TimeSpan(13, 0, 0) },
                new Lecture { Id = 5, FacultyId = 1, AcademicEmployeeId = 2, SubjectId = 5, LectureRoomId = 2, LectureDate = DateTime.Now.AddDays(-4), StartTime = new TimeSpan(14, 0, 0) },
            };

            var mockLecturesRepository = CreateMockLecturesRepository(lectures);
            var scheduleService = new ScheduleService(mockLecturesRepository.Object);

            // Act
            var response = await scheduleService.GetScheduleForTeacher(teacherId, lastDateOfPeriod, sortOrder, searchString);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(StatusCode.NoContent, response.StatusCode);
            Assert.Equal("0 items found", response.Description);
            Assert.Null(response.Data);
        }

        [Fact]
        public async Task GetScheduleForStudent_WithNoLectures_NoContent()
        {
            // Arrange
            var studentId = 2;
            var lastDateOfPeriod = DateTime.Now.AddDays(7); // Set the last date of the period to 7 days from now
            var sortOrder = "date_desc";
            var searchString = "";

            // Sample data to be returned by the mock repository
            var lectures = new List<Lecture>(); // No lectures for the student

            var mockLecturesRepository = new Mock<ILecturesRepository>();
            mockLecturesRepository
                .Setup(repo => repo.GetLecturesByStudentIdAsync(studentId))
                .ReturnsAsync(lectures);

            var scheduleService = new ScheduleService(mockLecturesRepository.Object);

            // Act
            var response = await scheduleService.GetScheduleForStudent(studentId, lastDateOfPeriod, sortOrder, searchString);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(StatusCode.NoContent, response.StatusCode);
            Assert.Equal("0 items found", response.Description);
            Assert.Null(response.Data);
        }

        [Fact]
        public async Task GetScheduleForStudent_WithDataSorteredByDate_Success()
        {
            // Arrange
            var studentId = 1;
            var lastDateOfPeriod = DateTime.Now.AddDays(7); // Set the last date of the period to 7 days from now
            var sortOrder = "date_desc";
            var searchString = "";

            // Sample data to be returned by the mock repository
            var mathSubject = new Subject { Id = 1, Name = "Math" };
            var physicsSubject = new Subject { Id = 2, Name = "Physics" };

            var lectures = new List<Lecture>
            {
                // Create sample lectures with various subjects and dates
                new Lecture { Id = 1, FacultyId = 1, AcademicEmployeeId = 2, SubjectId = 1, LectureRoomId = 1, LectureDate = DateTime.Now.AddDays(2), StartTime = new TimeSpan(9, 0, 0) },
                new Lecture { Id = 2, FacultyId = 1, AcademicEmployeeId = 2, SubjectId = 2, LectureRoomId = 1, LectureDate = DateTime.Now.AddDays(3), StartTime = new TimeSpan(10, 0, 0) },
                new Lecture { Id = 3, FacultyId = 1, AcademicEmployeeId = 2, SubjectId = 1, LectureRoomId = 1, LectureDate = DateTime.Now.AddDays(1), StartTime = new TimeSpan(11, 0, 0) },
                new Lecture { Id = 4, FacultyId = 1, AcademicEmployeeId = 2, SubjectId = 2, LectureRoomId = 2, LectureDate = DateTime.Now.AddDays(5), StartTime = new TimeSpan(13, 0, 0) },
                new Lecture { Id = 5, FacultyId = 1, AcademicEmployeeId = 2, SubjectId = 1, LectureRoomId = 2, LectureDate = DateTime.Now.AddDays(4), StartTime = new TimeSpan(14, 0, 0) },
            };

            var mockLecturesRepository = new Mock<ILecturesRepository>();
            mockLecturesRepository
                .Setup(repo => repo.GetLecturesByStudentIdAsync(studentId))
                .ReturnsAsync(lectures);

            // Set up the mock repository to return the associated Subject entities for each lecture
            mockLecturesRepository
                .Setup(repo => repo.GetLectureWithIncludePropertiesByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int lectureId) =>
                {
                    var lecture = lectures.FirstOrDefault(l => l.Id == lectureId);
                    if (lecture != null)
                    {
                        lecture.Subject = lecture.SubjectId switch
                        {
                            1 => mathSubject,
                            2 => physicsSubject,
                            _ => null,
                        };
                    }
                    return lecture;
                });

            var scheduleService = new ScheduleService(mockLecturesRepository.Object);

            // Act
            var response = await scheduleService.GetScheduleForStudent(studentId, lastDateOfPeriod, sortOrder, searchString);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(StatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Data);

            // Verify that all lectures are present in the response data
            Assert.Equal(lectures.Count, response.Data.Count());
            Assert.True(lectures.All(l => response.Data.Any(r => r.Id == l.Id)));
        }
    }
}
