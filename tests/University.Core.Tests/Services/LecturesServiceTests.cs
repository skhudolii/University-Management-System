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
            var newLectureVM = new NewLectureModel
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
                AcademicEmployee = new AcademicEmployee { FirstName = "John Doe" },
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
            var newLectureVM = new NewLectureModel
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
            _mockLecturesRepository.Verify(r => r.UpdateLectureAsync(It.IsAny<NewLectureModel>()), Times.Never);
        }
    }
}
