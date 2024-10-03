using Microsoft.Extensions.Logging;
using Moq;
using ProjectManager.Application.ProjectTask.Queries.GetAllTasksFromProject;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;

namespace ProjectManager.UnitTests.ProjectTask.Queries.GetAllTasksFromProject
{
    [TestClass] public class GetAllTasksFromProjectQueryHandlerTests : DefaultFixture
    {
        private MockRepository mockRepository;

        private Mock<IProjectRepository> mockProjectRepository;
        private Mock<ILogger<GetAllTasksFromProjectQueryHandler>> mockLogger;

        public GetAllTasksFromProjectQueryHandlerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Default);

            this.mockProjectRepository = this.mockRepository.Create<IProjectRepository>();
            this.mockLogger = this.mockRepository.Create<ILogger<GetAllTasksFromProjectQueryHandler>>();
        }

        private GetAllTasksFromProjectQueryHandler CreateGetAllTasksFromProjectQueryHandler()
        {
            return new GetAllTasksFromProjectQueryHandler(
                this.mockProjectRepository.Object,
                this.mockLogger.Object);
        }

        [Fact]
        public async Task Test_Success_Scenario()
        {
            //faça o setup seguindo o exemplo dos outros testes usando ProjectManagerFixture

            // Arrange
            var getAllTasksFromProjectQueryHandler = this.CreateGetAllTasksFromProjectQueryHandler();
            var request = ProjectManagerFixture.Create<GetAllTasksFromProjectQuery>();
            CancellationToken cancellationToken = default;

            //setup mocks
            mockProjectRepository.Setup(x => x.GetByIdAsNoTrackingAsync(It.IsAny<int>())).ReturnsAsync(ProjectManagerFixture.Create<Domain.Entities.Project>());

            // Act
            var result = await getAllTasksFromProjectQueryHandler.Handle(
                request,
                cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Data.Tasks.Count().Should().Be(3);
            result.Errors.Should().BeNullOrEmpty();
        }
    }
}
