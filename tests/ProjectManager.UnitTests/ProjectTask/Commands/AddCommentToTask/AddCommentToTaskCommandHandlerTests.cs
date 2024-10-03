using Microsoft.Extensions.Logging;
using Moq;
using ProjectManager.Application.ProjectTask.Commands.AddCommentToTask;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ProjectManager.UnitTests.ProjectTask.Commands.AddCommentToTask
{
    public class AddCommentToTaskCommandHandlerTests
    {
        private MockRepository mockRepository;

        private Mock<ICommentTaskRepository> mockCommentTaskRepository;
        private Mock<ILogger<AddCommentToTaskCommandHandler>> mockLogger;
        private Mock<ITaskHistoryRepository> mockTaskHistoryRepository;

        public AddCommentToTaskCommandHandlerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockCommentTaskRepository = this.mockRepository.Create<ICommentTaskRepository>();
            this.mockLogger = this.mockRepository.Create<ILogger<AddCommentToTaskCommandHandler>>();
            this.mockTaskHistoryRepository = this.mockRepository.Create<ITaskHistoryRepository>();
        }

        private AddCommentToTaskCommandHandler CreateAddCommentToTaskCommandHandler()
        {
            return new AddCommentToTaskCommandHandler(
                this.mockCommentTaskRepository.Object,
                this.mockLogger.Object,
                this.mockTaskHistoryRepository.Object);
        }

        [Fact]
        public async Task Handle_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var addCommentToTaskCommandHandler = this.CreateAddCommentToTaskCommandHandler();
            AddCommentToTaskCommand request = null;
            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

            // Act
            var result = await addCommentToTaskCommandHandler.Handle(
                request,
                cancellationToken);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}
