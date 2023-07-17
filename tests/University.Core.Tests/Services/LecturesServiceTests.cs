using Moq;
using System.Linq.Expressions;
using University.Core.Entities;
using University.Core.Enums;
using University.Core.Repositories;
using University.Core.Services;
using University.Core.ViewModels.LectureVM;

namespace University.Core.Tests.Services
{
    public class LecturesServiceTests
    {
        private readonly Mock<ILecturesRepository> _mockLecturesRepository;

        public LecturesServiceTests()
        {
            _mockLecturesRepository = new Mock<ILecturesRepository>();
        }

        [Fact]
        public async Task UpdateLecture_ShouldUpdateExistingLecture_WhenValidDataIsProvided()
        {
            // Arrange
            var lectureId = 1;
            var newLectureVM = new NewLectureVM
            {
                Id = lectureId,
                LectureDate = new DateTime(2023, 7, 10),
                StartTime = TimeSpan.Parse("14:00"),
                EndTime = TimeSpan.Parse("16:00"),
                FacultyId = 1,
                SubjectId = 2,
                LectureRoomId = 3,
                AcademicEmployeeId = 4,
                GroupIds = new List<int> { 1, 2 }
            };

            var existingLecture = new Lecture
            {
                Id = lectureId,
                FacultyId = 1,
                Subject = new Subject { Name = "Mathematics" },
                AcademicEmployee = new AcademicEmployee { FullName = "John Doe" },
                LectureDate = new DateTime(2023, 7, 1),
                StartTime = TimeSpan.Parse("10:00"),
                EndTime = TimeSpan.Parse("12:00")
            };

            _mockLecturesRepository.Setup(r => r.GetByIdAsync(lectureId)).ReturnsAsync(existingLecture);
            _mockLecturesRepository.Setup(r => r.UpdateLectureAsync(newLectureVM)).Returns(Task.CompletedTask);

            var lecturesService = new LecturesService(_mockLecturesRepository.Object);

            // Act
            var response = await lecturesService.UpdateLecture(newLectureVM);

            // Assert
            Assert.Equal(StatusCode.OK, response.StatusCode);
            Assert.Equal("Lecture successfully updated", response.Description);

            _mockLecturesRepository.Verify(r => r.GetByIdAsync(lectureId), Times.Once);
            _mockLecturesRepository.Verify(r => r.UpdateLectureAsync(newLectureVM), Times.Once);
        }

        [Fact]
        public async Task UpdateLecture_ShouldReturnNotFound_WhenLectureDoesNotExist()
        {
            // Arrange
            var lectureId = 1;
            var newLectureVM = new NewLectureVM
            {
                Id = lectureId,
                LectureDate = new DateTime(2023, 7, 10),
                StartTime = TimeSpan.Parse("14:00"),
                EndTime = TimeSpan.Parse("16:00"),
                FacultyId = 1,
                SubjectId = 2,
                LectureRoomId = 3,
                AcademicEmployeeId = 4,
                GroupIds = new List<int> { 1, 2 }
            };

            _mockLecturesRepository.Setup(r => r.GetByIdAsync(lectureId)).ReturnsAsync((Lecture)null);

            var lecturesService = new LecturesService(_mockLecturesRepository.Object);

            // Act
            var response = await lecturesService.UpdateLecture(newLectureVM);

            // Assert
            Assert.Null(response.Data);
            Assert.Equal(StatusCode.NotFound, response.StatusCode);
            Assert.Equal("Not found", response.Description);
            _mockLecturesRepository.Verify(r => r.GetByIdAsync(lectureId), Times.Once);
            _mockLecturesRepository.Verify(r => r.UpdateLectureAsync(It.IsAny<NewLectureVM>()), Times.Never);
        }


        [Fact]
        public async Task Filter_ShouldReturnFilteredLecturesBySubject_WhenSearchStringIsValid()
        {
            // Arrange
            var searchString = "Mathematics";
            var lectures = new List<Lecture>
            {
                new Lecture { Id = 1, FacultyId = 1, Subject = new Subject { Name = "Mathematics" }, AcademicEmployee = new AcademicEmployee { FullName = "John Doe" }, LectureDate = new DateTime(2023, 7, 1), StartTime = TimeSpan.Parse("10:00"), EndTime = TimeSpan.Parse("12:00") },
                new Lecture { Id = 2, FacultyId = 1, Subject = new Subject { Name = "Physics" }, AcademicEmployee = new AcademicEmployee { FullName = "Jane Smith" }, LectureDate = new DateTime(2023, 7, 2), StartTime = TimeSpan.Parse("14:00"), EndTime = TimeSpan.Parse("16:00") },
                new Lecture { Id = 3, FacultyId = 1, Subject = new Subject { Name = "Mathematical Analysis" }, AcademicEmployee = new AcademicEmployee { FullName = "Bob Johnson" }, LectureDate = new DateTime(2023, 7, 3), StartTime = TimeSpan.Parse("10:00"), EndTime = TimeSpan.Parse("12:00") }
            };

            _mockLecturesRepository.Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<Lecture, object>>[]>()))
                .ReturnsAsync(lectures.AsEnumerable());

            var lecturesService = new LecturesService(_mockLecturesRepository.Object);

            // Act
            var response = await lecturesService.Filter(searchString);

            // Assert
            Assert.Single(response.Data);
            Assert.Contains(response.Data, l => l.Subject.Name == "Mathematics");
            Assert.DoesNotContain(response.Data, l => l.Subject.Name == "Physics");
            Assert.DoesNotContain(response.Data, l => l.Subject.Name == "Mathematical Analysis");
        }

        [Fact]
        public async Task Filter_ShouldReturnFilteredLecturesByTeacher_WhenSearchStringIsValid()
        {
            // Arrange
            var searchString = "John";
            var lectures = new List<Lecture>
            {
                new Lecture { Id = 1, FacultyId = 1, Subject = new Subject { Name = "Mathematics" }, AcademicEmployee = new AcademicEmployee { FullName = "John Doe" }, LectureDate = new DateTime(2023, 7, 1), StartTime = TimeSpan.Parse("10:00"), EndTime = TimeSpan.Parse("12:00") },
                new Lecture { Id = 2, FacultyId = 1, Subject = new Subject { Name = "Physics" }, AcademicEmployee = new AcademicEmployee { FullName = "Jane Smith" }, LectureDate = new DateTime(2023, 7, 2), StartTime = TimeSpan.Parse("14:00"), EndTime = TimeSpan.Parse("16:00") },
                new Lecture { Id = 3, FacultyId = 1, Subject = new Subject { Name = "Mathematical Analysis" }, AcademicEmployee = new AcademicEmployee { FullName = "John Deere" }, LectureDate = new DateTime(2023, 7, 3), StartTime = TimeSpan.Parse("10:00"), EndTime = TimeSpan.Parse("12:00") }
            };

            _mockLecturesRepository.Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<Lecture, object>>[]>()))
                .ReturnsAsync(lectures);

            var lecturesService = new LecturesService(_mockLecturesRepository.Object);

            // Act
            var response = await lecturesService.Filter(searchString);

            // Assert
            Assert.Equal(2, response.Data.Count());
            Assert.Contains(response.Data, l => l.AcademicEmployee.FullName == "John Doe");
            Assert.Contains(response.Data, l => l.AcademicEmployee.FullName == "John Deere");
            Assert.DoesNotContain(response.Data, l => l.AcademicEmployee.FullName == "Jane Smith");
        }

        [Fact]
        public async Task Filter_ShouldReturnFilteredLecturesByLectureDate_WhenSearchStringIsValid()
        {
            // Arrange
            var searchString = "02.07"; // Consider your regional settings!
            var lectures = new List<Lecture>
            { 
                new Lecture { Id = 1, FacultyId = 1, Subject = new Subject { Name = "Mathematics" }, AcademicEmployee = new AcademicEmployee { FullName = "John Doe" }, LectureDate = new DateTime(2023, 7, 1), StartTime = TimeSpan.Parse("10:00"), EndTime = TimeSpan.Parse("12:00") }, 
                new Lecture { Id = 2, FacultyId = 1, Subject = new Subject { Name = "Physics" }, AcademicEmployee = new AcademicEmployee { FullName = "Jane Smith" }, LectureDate = new DateTime(2023, 7, 2), StartTime = TimeSpan.Parse("14:00"), EndTime = TimeSpan.Parse("16:00") }, 
                new Lecture { Id = 3, FacultyId = 1, Subject = new Subject { Name = "Mathematical Analysis" }, AcademicEmployee = new AcademicEmployee { FullName = "John Deere" }, LectureDate = new DateTime(2023, 7, 3), StartTime = TimeSpan.Parse("10:00"), EndTime = TimeSpan.Parse("12:00") }
            };

            _mockLecturesRepository.Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<Lecture, object>>[]>()))
                .ReturnsAsync(lectures);

            var lecturesService = new LecturesService(_mockLecturesRepository.Object);

            // Act
            var response = await lecturesService.Filter(searchString);

            // Assert
            Assert.Single(response.Data);
            Assert.Contains(response.Data, l => l.LectureDate == new DateTime(2023, 7, 2));
            Assert.DoesNotContain(response.Data, l => l.LectureDate != new DateTime(2023, 7, 2));
        }

        [Fact]
        public async Task Filter_ShouldReturnNoResults_WhenSearchStringDoesNotMatchAnyLectures()
        {
            // Arrange
            var searchString = "Chemistry";
            var lectures = new List<Lecture>
            { 
                new Lecture { Id = 1, FacultyId = 1, Subject = new Subject { Name = "Mathematics" }, AcademicEmployee = new AcademicEmployee { FullName = "John Doe" }, LectureDate = new DateTime(2023, 7, 1), StartTime = TimeSpan.Parse("10:00"), EndTime = TimeSpan.Parse("12:00") }, 
                new Lecture { Id = 2, FacultyId = 1, Subject = new Subject { Name = "Physics" }, AcademicEmployee = new AcademicEmployee { FullName = "Jane Smith" }, LectureDate = new DateTime(2023, 7, 2), StartTime = TimeSpan.Parse("14:00"), EndTime = TimeSpan.Parse("16:00") }, 
                new Lecture { Id = 3, FacultyId = 1, Subject = new Subject { Name = "Mathematical Analysis" }, AcademicEmployee = new AcademicEmployee { FullName = "John Deere" }, LectureDate = new DateTime(2023, 7, 3), StartTime = TimeSpan.Parse("10:00"), EndTime = TimeSpan.Parse("12:00") }
            };

            _mockLecturesRepository.Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<Lecture, object>>[]>()))
                .ReturnsAsync(lectures);

            var lecturesService = new LecturesService(_mockLecturesRepository.Object);

            // Act
            var response = await lecturesService.Filter(searchString);

            // Assert
            Assert.Null(response.Data);
            Assert.Contains("0 items found", response.Description);
            Assert.Equal(StatusCode.NoContent, response.StatusCode);
        }

    }
}
