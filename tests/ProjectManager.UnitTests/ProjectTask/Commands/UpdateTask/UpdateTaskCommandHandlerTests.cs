using Microsoft.Extensions.Logging;
using Moq;
using ProjectManager.Application.ProjectTask.Commands.UpdateTask;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ProjectManager.UnitTests.ProjectTask.Commands.UpdateTask
{
    public class UpdateTaskCommandHandlerTests
    {
        private MockRepository mockRepository;

        private Mock<ITaskRepository> mockTaskRepository;
        private Mock<ILogger<UpdateTaskCommandHandler>> mockLogger;
        private Mock<ITaskHistoryRepository> mockTaskHistoryRepository;

        public UpdateTaskCommandHandlerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockTaskRepository = this.mockRepository.Create<ITaskRepository>();
            this.mockLogger = this.mockRepository.Create<ILogger<UpdateTaskCommandHandler>>();
            this.mockTaskHistoryRepository = this.mockRepository.Create<ITaskHistoryRepository>();
        }

        private UpdateTaskCommandHandler CreateUpdateTaskCommandHandler()
        {
            return new UpdateTaskCommandHandler(
                this.mockTaskRepository.Object,
                this.mockLogger.Object,
                this.mockTaskHistoryRepository.Object);
        }

        [Fact]
        public async Task Handle_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var updateTaskCommandHandler = this.CreateUpdateTaskCommandHandler();
            UpdateTaskCommand request = null;
            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

            // Act
            var result = await updateTaskCommandHandler.Handle(
                request,
                cancellationToken);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}
