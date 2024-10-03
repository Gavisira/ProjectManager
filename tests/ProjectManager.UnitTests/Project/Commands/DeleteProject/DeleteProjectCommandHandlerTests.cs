using Microsoft.Extensions.Logging;
using Moq;
using ProjectManager.Application.Project.DeleteProject;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ProjectManager.UnitTests.Project.Commands.DeleteProject
{
    public class DeleteProjectCommandHandlerTests
    {
        private MockRepository mockRepository;

        private Mock<IProjectRepository> mockProjectRepository;
        private Mock<ILogger<DeleteProjectCommandHandler>> mockLogger;

        public DeleteProjectCommandHandlerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockProjectRepository = this.mockRepository.Create<IProjectRepository>();
            this.mockLogger = this.mockRepository.Create<ILogger<DeleteProjectCommandHandler>>();
        }

        private DeleteProjectCommandHandler CreateDeleteProjectCommandHandler()
        {
            return new DeleteProjectCommandHandler(
                this.mockProjectRepository.Object,
                this.mockLogger.Object);
        }

        [Fact]
        public async Task Handle_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var deleteProjectCommandHandler = this.CreateDeleteProjectCommandHandler();
            DeleteProjectCommand request = null;
            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

            // Act
            var result = await deleteProjectCommandHandler.Handle(
                request,
                cancellationToken);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}
