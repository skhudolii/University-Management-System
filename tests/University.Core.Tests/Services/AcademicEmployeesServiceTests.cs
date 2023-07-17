using Moq;
using System.Linq.Expressions;
using University.Core.Entities;
using University.Core.Enums;
using University.Core.Repositories;
using University.Core.Services;

namespace University.Core.Tests.Services
{
    public class AcademicEmployeesServiceTests
    {
        private readonly Mock<IAcademicEmployeesRepository> _mockAcademicEmployeesRepository;
        private readonly Mock<IFacultiesRepository> _mockFacultiesRepository;

        public AcademicEmployeesServiceTests()
        {
            _mockAcademicEmployeesRepository = new Mock<IAcademicEmployeesRepository>();
            _mockFacultiesRepository = new Mock<IFacultiesRepository>();
        }

        [Fact]
        public async Task GetNewAcademicEmployeeDropdownsValues_ShouldReturnDropdownValues()
        {
            // Arrange
            var faculties = new List<Faculty>
            {
                new Faculty { Id = 1, Name = "F1", Logo = "L1" },
                new Faculty { Id = 2, Name = "F2", Logo = "L2" }
            };

            _mockFacultiesRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(faculties);

            var academicEmployeesService = new AcademicEmployeesService(
                Mock.Of<IAcademicEmployeesRepository>(),
                _mockFacultiesRepository.Object
            );

            // Act
            var result = await academicEmployeesService.GetNewAcademicEmployeeDropdownsValues();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCode.OK, result.StatusCode);
            Assert.NotNull(result.Data);
            Assert.Equal(faculties.Count, result.Data.Faculties.Count());
            Assert.Equal(faculties[0].Id, result.Data.Faculties.First().Id);
            Assert.Equal(faculties[0].Name, result.Data.Faculties.First().Name);
        }

        [Fact]
        public async Task GetAcademicEmployeeWithIncludePropertiesById_ShouldReturnAcademicEmployeeWithFaculty()
        {
            // Arrange
            var facultyId = 1;
            var academicEmployeeId = 1;
            var faculty = new Faculty { Id = facultyId, Name = "F1", Logo = "L1" };
            var academicEmployee = new AcademicEmployee { Id = academicEmployeeId, FacultyId = facultyId, Faculty = faculty };

            _mockAcademicEmployeesRepository.Setup(repo => repo.GetByIdAsync(academicEmployeeId, It.IsAny<Expression<Func<AcademicEmployee, object>>>()))
                .ReturnsAsync(academicEmployee);

            var academicEmployeesService = new AcademicEmployeesService(
                _mockAcademicEmployeesRepository.Object,
                Mock.Of<IFacultiesRepository>()
            );

            // Act
            var result = await academicEmployeesService.GetAcademicEmployeeWithIncludePropertiesById(academicEmployeeId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCode.OK, result.StatusCode);
            Assert.NotNull(result.Data);
            Assert.Equal(academicEmployeeId, result.Data.Id);
            Assert.Equal(facultyId, result.Data.Faculty.Id);
            Assert.Equal(faculty.Name, result.Data.Faculty.Name);
        }

        [Fact]
        public async Task GetAcademicEmployeeWithIncludePropertiesById_IfNotFound_ReturnNotFoundResponse()
        {
            // Arrange
            int academicEmployeeId = 1;
            AcademicEmployee academicEmployee = null;

            _mockAcademicEmployeesRepository.Setup(repo => repo.GetByIdAsync(academicEmployeeId, It.IsAny<Expression<Func<AcademicEmployee, object>>>()))
                .ReturnsAsync(academicEmployee);

            var academicEmployeesService = new AcademicEmployeesService(
                _mockAcademicEmployeesRepository.Object,
                Mock.Of<IFacultiesRepository>()
            );

            // Act
            var result = await academicEmployeesService.GetAcademicEmployeeWithIncludePropertiesById(academicEmployeeId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCode.NotFound, result.StatusCode);
            Assert.Null(result.Data);
        }
    }
}
