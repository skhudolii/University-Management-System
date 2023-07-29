using Moq;
using University.Core.Entities;
using University.Core.Enums;
using University.Core.Repositories;
using University.Core.Services;

namespace University.Core.Tests.Services
{
    public class GroupsServiceTests
    {
        private readonly Mock<IGroupsRepository> _mockGroupsRepository;
        private readonly Mock<IFacultiesRepository> _mockFacultiesRepository;

        public GroupsServiceTests()
        {
            _mockGroupsRepository = new Mock<IGroupsRepository>();
            _mockFacultiesRepository = new Mock<IFacultiesRepository>();
        }

        [Fact]
        public async Task DeleteGroup_NoStudentsInGroup_ReturnSuccessResponse()
        {
            // Arrange
            var groupId = 1;

            // Create an instance of the GroupsService using the mocked repositories
            var groupsService = new GroupsService(_mockGroupsRepository.Object, _mockFacultiesRepository.Object);

            // Mock the GetByIdAsync method to return a group with no students
            _mockGroupsRepository.Setup(repo => repo.GetByIdAsync(groupId)).ReturnsAsync(new Group
            {
                Id = groupId,
                Students = new List<Student>()
            });

            // Act
            var response = await groupsService.DeleteGroup(groupId);

            // Assert
            Assert.True(response.Data); // Expected to be true since the group can be deleted
            Assert.Equal("Group successfully deleted", response.Description);
            Assert.Equal(StatusCode.OK, response.StatusCode);
            _mockGroupsRepository.Verify(repo => repo.DeleteAsync(groupId), Times.Once);
        }

        [Fact]
        public async Task DeleteGroup_StudentInGroup_ReturnErrorResponse()
        {
            // Arrange
            var groupId = 1;

            // Create an instance of the GroupsService using the mocked repositories
            var groupsService = new GroupsService(_mockGroupsRepository.Object, _mockFacultiesRepository.Object);

            // Mock the GetByIdAsync method to return a group with students
            _mockGroupsRepository.Setup(repo => repo.GetByIdAsync(groupId)).ReturnsAsync(new Group
            {
                Id = groupId,
                Students = new List<Student> { new Student() }
            });

            // Act
            var response = await groupsService.DeleteGroup(groupId);

            // Assert
            Assert.False(response.Data); // Expected to be false since the group cannot be deleted
            Assert.Equal("Group can not be deleted if there is at least one student in this group", response.Description);
            Assert.Equal(StatusCode.PreconditionFailed, response.StatusCode);
            _mockGroupsRepository.Verify(repo => repo.DeleteAsync(groupId), Times.Never);
        }
    }
}
