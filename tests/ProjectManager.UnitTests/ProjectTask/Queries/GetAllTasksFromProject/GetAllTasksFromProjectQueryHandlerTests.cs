using Microsoft.Extensions.Logging;
using Moq;
using ProjectManager.Application.ProjectTask.Queries.GetAllTasksFromProject;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ProjectManager.UnitTests.ProjectTask.Queries.GetAllTasksFromProject
{
    public class GetAllTasksFromProjectQueryHandlerTests
    {
        private MockRepository mockRepository;

        private Mock<IProjectRepository> mockProjectRepository;
        private Mock<ILogger<GetAllTasksFromProjectQueryHandler>> mockLogger;

        public GetAllTasksFromProjectQueryHandlerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

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
        public async Task Handle_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var getAllTasksFromProjectQueryHandler = this.CreateGetAllTasksFromProjectQueryHandler();
            GetAllTasksFromProjectQuery request = null;
            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

            // Act
            var result = await getAllTasksFromProjectQueryHandler.Handle(
                request,
                cancellationToken);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}
