using Microsoft.Extensions.Logging;
using Moq;
using ProjectManager.Application.Project.UpdateProject;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ProjectManager.UnitTests.Project.Commands.UpdateProject
{
    public class UpdateProjectCommandHandlerTests
    {
        private MockRepository mockRepository;

        private Mock<IProjectRepository> mockProjectRepository;
        private Mock<ILogger<UpdateProjectCommandHandler>> mockLogger;

        public UpdateProjectCommandHandlerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockProjectRepository = this.mockRepository.Create<IProjectRepository>();
            this.mockLogger = this.mockRepository.Create<ILogger<UpdateProjectCommandHandler>>();
        }

        private UpdateProjectCommandHandler CreateUpdateProjectCommandHandler()
        {
            return new UpdateProjectCommandHandler(
                this.mockProjectRepository.Object,
                this.mockLogger.Object);
        }

        [Fact]
        public async Task Handle_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var updateProjectCommandHandler = this.CreateUpdateProjectCommandHandler();
            UpdateProjectCommand request = null;
            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

            // Act
            var result = await updateProjectCommandHandler.Handle(
                request,
                cancellationToken);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}
