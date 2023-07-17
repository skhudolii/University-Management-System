using Moq;
using University.Core.Entities;
using University.Core.Enums;
using University.Core.Repositories;
using University.Core.Services;

namespace University.Core.Tests.Services
{
    public class LectureCascadingDropdownsServiceTests
    {
        private readonly Mock<IAcademicEmployeesRepository> _mockAcademicEmployeesRepository;
        private readonly Mock<IGroupsRepository> _mockGroupsRepository;
        private readonly Mock<ILectureRoomsRepository> _mockLectureRoomsRepository;
        private readonly Mock<ISubjectsRepository> _mockSubjectsRepository;
        private readonly Mock<IFacultiesRepository> _mockFacultiesRepository;

        public LectureCascadingDropdownsServiceTests()
        {
            _mockAcademicEmployeesRepository = new Mock<IAcademicEmployeesRepository>();
            _mockGroupsRepository = new Mock<IGroupsRepository>();
            _mockLectureRoomsRepository = new Mock<ILectureRoomsRepository>();
            _mockSubjectsRepository = new Mock<ISubjectsRepository>();
            _mockFacultiesRepository = new Mock<IFacultiesRepository>();
        }

        [Fact]
        public async Task GetDependentDropdownsValues_ReturnValidResponse()
        {
            // Arrange
            var academicEmployees = new List<AcademicEmployee>
            {
                new AcademicEmployee { Id = 1, FullName = "FN1", FacultyId = 1 },
                new AcademicEmployee { Id = 2, FullName = "FN2", FacultyId = 2 }
            };

            var groups = new List<Group>
            {
                new Group { Id = 1, Name = "G1", FacultyId = 1 },
                new Group { Id = 2, Name = "G2", FacultyId = 2 }
            };

            var lectureRooms = new List<LectureRoom>
            {
                new LectureRoom { Id = 1, Name = "R1", FacultyId = 1 },
                new LectureRoom { Id = 2, Name = "R2", FacultyId = 2 }
            };

            var subjects = new List<Subject>
            {
                new Subject { Id = 1, Name = "S1", FacultyId = 1 },
                new Subject { Id = 2, Name = "S2", FacultyId = 2 }
            };

            // Mock the repositories
            _mockAcademicEmployeesRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(academicEmployees);
            _mockGroupsRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(groups);
            _mockLectureRoomsRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(lectureRooms);
            _mockSubjectsRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(subjects);

            // Create an instance of the service with mocked repositories
            var service = new LectureCascadingDropdownsService(
                _mockAcademicEmployeesRepository.Object,
                null, // Pass null for FacultiesRepository that is not used in this test
                _mockGroupsRepository.Object,
                _mockLectureRoomsRepository.Object,
                _mockSubjectsRepository.Object
            );

            // Act
            var result = await service.GetDependentDropdownsValues();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCode.OK, result.StatusCode);
            Assert.NotNull(result.Data);
            Assert.Equal(academicEmployees.OrderBy(e => e.FullName).ToList(), result.Data.AcademicEmployees);
            Assert.Equal(groups.OrderBy(g => g.Name).ToList(), result.Data.Groups);
            Assert.Equal(lectureRooms.OrderBy(r => r.Name).ToList(), result.Data.LectureRooms);
            Assert.Equal(subjects.OrderBy(s => s.Name).ToList(), result.Data.Subjects);
        }

        [Fact]
        public async Task GetDependentDropdownsValues_ExceptionThrown_ReturnErrorResponse()
        {
            // Arrange
            _mockAcademicEmployeesRepository.Setup(r => r.GetAllAsync()).ThrowsAsync(new Exception("An error occurred."));

            var service = new LectureCascadingDropdownsService(
                _mockAcademicEmployeesRepository.Object,
                null, // Pass null for other repositories that are not used in this test
                null,
                null,
                null
            );

            // Act
            var result = await service.GetDependentDropdownsValues();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCode.InternalServerError, result.StatusCode);
            Assert.Null(result.Data);
            Assert.Contains("[LectureCascadingDropdownsService.GetDependentDropdownsValues]", result.Description);
            Assert.Contains("An error occurred.", result.Description);
        }
    }
}
