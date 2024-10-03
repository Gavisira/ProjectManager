using Microsoft.Extensions.Logging;
using Moq;
using ProjectManager.Application.Project.CreateProject;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ProjectManager.UnitTests.Project.Commands.CreateProject
{
    public class CreateProjectCommandHandlerTests
    {
        private MockRepository mockRepository;

        private Mock<IProjectRepository> mockProjectRepository;
        private Mock<ILogger<CreateProjectCommandHandler>> mockLogger;

        public CreateProjectCommandHandlerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockProjectRepository = this.mockRepository.Create<IProjectRepository>();
            this.mockLogger = this.mockRepository.Create<ILogger<CreateProjectCommandHandler>>();
        }

        private CreateProjectCommandHandler CreateCreateProjectCommandHandler()
        {
            return new CreateProjectCommandHandler(
                this.mockProjectRepository.Object,
                this.mockLogger.Object);
        }

        [Fact]
        public async Task Handle_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var createProjectCommandHandler = this.CreateCreateProjectCommandHandler();
            CreateProjectCommand request = null;
            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

            // Act
            var result = await createProjectCommandHandler.Handle(
                request,
                cancellationToken);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}
