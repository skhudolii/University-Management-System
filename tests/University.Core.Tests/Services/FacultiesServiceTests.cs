using Moq;
using University.Core.Entities;
using University.Core.Enums;
using University.Core.Repositories;
using University.Core.Services;
using University.Core.ViewModels.FacultyVM;

namespace University.Core.Tests.Services
{
    public class FacultiesServiceTests
    {
        private readonly Mock<IFacultiesRepository> _mockFacultiesRepository;

        public FacultiesServiceTests()
        {
            _mockFacultiesRepository = new Mock<IFacultiesRepository>();
        }

        [Fact]
        public async Task GetFacultiesList_ReturnAllFaculties()
        {
            // Arrange
            var facultiesService = new FacultiesService(_mockFacultiesRepository.Object);

            // Create sample faculty data
            var faculty1 = new Faculty { Id = 1, Name = "F1", Logo = "L1" };
            var faculty2 = new Faculty { Id = 2, Name = "F2", Logo = "L2" };
            var faculty3 = new Faculty { Id = 3, Name = "F3", Logo = "L3" };
            var facultyList = new List<Faculty> { faculty1, faculty2, faculty3 };

            // Setup the mock repository to return the sample faculty list
            _mockFacultiesRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(facultyList);

            // Act
            var result = await facultiesService.GetFacultiesList("");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCode.OK, result.StatusCode);
            Assert.Equal(facultyList.Count, result.Data.ToList().Count);
            Assert.Equal(facultyList, result.Data);
        }

        [Fact]
        public async Task GetFacultyById_ReturnCorrectFaculty()
        {
            // Arrange
            var facultiesService = new FacultiesService(_mockFacultiesRepository.Object);

            // Create sample faculty data
            var faculty1 = new Faculty { Id = 1, Name = "F1", Logo = "L1" };
            var faculty2 = new Faculty { Id = 2, Name = "F2", Logo = "L2" };
            var faculty3 = new Faculty { Id = 3, Name = "F3", Logo = "L3" };

            // Setup the mock repository to return the correct faculty when GetByIdAsync is called
            _mockFacultiesRepository.Setup(repo => repo.GetByIdAsync(2)).ReturnsAsync(faculty2);

            // Act
            var result = await facultiesService.GetFacultyById(2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCode.OK, result.StatusCode);
            Assert.Equal(faculty2.Id, result.Data.Id);
            Assert.Equal(faculty2.Name, result.Data.Name);
        }

        [Fact]
        public async Task GetFacultyById_FacultyNotFound_ReturnNotFoundResponse()
        {
            // Arrange
            var facultyId = 1;
            _mockFacultiesRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Faculty)null);

            var facultiesService = new FacultiesService(_mockFacultiesRepository.Object);

            // Act
            var response = await facultiesService.GetFacultyById(facultyId);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(StatusCode.NotFound, response.StatusCode);
            Assert.Equal("Not found", response.Description);
            Assert.Null(response.Data);

            _mockFacultiesRepository.Verify(repo => repo.GetByIdAsync(facultyId), Times.Once);
        }

        [Fact]
        public async Task AddNewFaculty_ValidModel_ReturnsSuccessResponse()
        {
            // Arrange
            var model = new NewFacultyModel { Name = "F1", Logo = "L1" };
            _mockFacultiesRepository.Setup(repo => repo.AddAsync(It.IsAny<Faculty>())).Returns(Task.CompletedTask);
            var facultiesService = new FacultiesService(_mockFacultiesRepository.Object);

            // Act
            var response = await facultiesService.AddNewFaculty(model);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(StatusCode.OK, response.StatusCode);
            Assert.Equal("New Faculty successfully added", response.Description);
            Assert.NotNull(response.Data);
            Assert.Equal(model.Name, response.Data.Name);
            Assert.Equal(model.Logo, response.Data.Logo);

            _mockFacultiesRepository.Verify(repo => repo.AddAsync(It.IsAny<Faculty>()), Times.Once);
        }

        [Fact]
        public async Task UpdateFaculty_ValidModel_ReturnSuccessResponse()
        {
            // Arrange
            var model = new NewFacultyModel { Id = 1, Name = "F1 - Updated", Logo = "L1 - Updated" };
            var existingFaculty = new Faculty { Id = 1, Name = "F1", Logo = "L1" };

            _mockFacultiesRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(existingFaculty);
            _mockFacultiesRepository.Setup(repo => repo.UpdateAsync(It.IsAny<int>(), It.IsAny<Faculty>())).Returns(Task.CompletedTask);

            var facultiesService = new FacultiesService(_mockFacultiesRepository.Object);

            // Act
            var response = await facultiesService.UpdateFaculty(model);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(StatusCode.OK, response.StatusCode);
            Assert.Equal("Faculty successfully updated", response.Description);

            _mockFacultiesRepository.Verify(repo => repo.GetByIdAsync(model.Id), Times.Once);
            _mockFacultiesRepository.Verify(repo => repo.UpdateAsync(model.Id, It.Is<Faculty>(f => f.Name == model.Name && f.Logo == model.Logo)), Times.Once);
        }

        [Fact]
        public async Task DeleteFaculty_FacultyExists_ReturnSuccessResponse()
        {
            // Arrange
            var facultyId = 1;
            var existingFaculty = new Faculty { Id = facultyId, Name = "F1", Logo = "L1" };

            _mockFacultiesRepository.Setup(repo => repo.GetByIdAsync(facultyId)).ReturnsAsync(existingFaculty);
            _mockFacultiesRepository.Setup(repo => repo.DeleteAsync(facultyId)).Returns(Task.CompletedTask);

            var facultiesService = new FacultiesService(_mockFacultiesRepository.Object);

            // Act
            var response = await facultiesService.DeleteFaculty(facultyId);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(StatusCode.OK, response.StatusCode);
            Assert.Equal("Faculty successfully deleted", response.Description);
            Assert.True(response.Data);

            _mockFacultiesRepository.Verify(repo => repo.GetByIdAsync(facultyId), Times.Once);
            _mockFacultiesRepository.Verify(repo => repo.DeleteAsync(facultyId), Times.Once);
        }

        [Fact]
        public async Task DeleteFaculty_FacultyNotFound_ReturnNotFoundResponse()
        {
            // Arrange
            var facultyId = 1;
            _mockFacultiesRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Faculty)null);

            var facultiesService = new FacultiesService(_mockFacultiesRepository.Object);

            // Act
            var response = await facultiesService.DeleteFaculty(facultyId);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(StatusCode.NotFound, response.StatusCode);
            Assert.Equal("Not found", response.Description);
            Assert.False(response.Data);

            _mockFacultiesRepository.Verify(repo => repo.GetByIdAsync(facultyId), Times.Once);
            _mockFacultiesRepository.Verify(repo => repo.DeleteAsync(It.IsAny<int>()), Times.Never);
        }
    }
}
