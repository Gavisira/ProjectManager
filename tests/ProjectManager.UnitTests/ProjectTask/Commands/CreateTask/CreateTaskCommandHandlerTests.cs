using Microsoft.Extensions.Logging;
using Moq;
using ProjectManager.Application.ProjectTask.Commands.CreateTask;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ProjectManager.UnitTests.ProjectTask.Commands.CreateTask
{
    public class CreateTaskCommandHandlerTests
    {
        private MockRepository mockRepository;

        private Mock<ITaskRepository> mockTaskRepository;
        private Mock<ILogger<CreateTaskCommandHandler>> mockLogger;
        private Mock<ITaskHistoryRepository> mockTaskHistoryRepository;

        public CreateTaskCommandHandlerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockTaskRepository = this.mockRepository.Create<ITaskRepository>();
            this.mockLogger = this.mockRepository.Create<ILogger<CreateTaskCommandHandler>>();
            this.mockTaskHistoryRepository = this.mockRepository.Create<ITaskHistoryRepository>();
        }

        private CreateTaskCommandHandler CreateCreateTaskCommandHandler()
        {
            return new CreateTaskCommandHandler(
                this.mockTaskRepository.Object,
                this.mockLogger.Object,
                this.mockTaskHistoryRepository.Object);
        }

        [Fact]
        public async Task Handle_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var createTaskCommandHandler = this.CreateCreateTaskCommandHandler();
            CreateTaskCommand request = null;
            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

            // Act
            var result = await createTaskCommandHandler.Handle(
                request,
                cancellationToken);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}
