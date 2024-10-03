using Microsoft.Extensions.Logging;
using Moq;
using ProjectManager.Application.ProjectTask.Commands.DeleteTask;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ProjectManager.UnitTests.ProjectTask.Commands.DeleteTask
{
    public class DeleteTaskCommandHandlerTests
    {
        private MockRepository mockRepository;

        private Mock<ITaskRepository> mockTaskRepository;
        private Mock<ILogger<DeleteTaskCommandHandler>> mockLogger;
        private Mock<ITaskHistoryRepository> mockTaskHistoryRepository;

        public DeleteTaskCommandHandlerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockTaskRepository = this.mockRepository.Create<ITaskRepository>();
            this.mockLogger = this.mockRepository.Create<ILogger<DeleteTaskCommandHandler>>();
            this.mockTaskHistoryRepository = this.mockRepository.Create<ITaskHistoryRepository>();
        }

        private DeleteTaskCommandHandler CreateDeleteTaskCommandHandler()
        {
            return new DeleteTaskCommandHandler(
                this.mockTaskRepository.Object,
                this.mockLogger.Object,
                this.mockTaskHistoryRepository.Object);
        }

        [Fact]
        public async Task Handle_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var deleteTaskCommandHandler = this.CreateDeleteTaskCommandHandler();
            DeleteTaskCommand request = null;
            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

            // Act
            var result = await deleteTaskCommandHandler.Handle(
                request,
                cancellationToken);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}
