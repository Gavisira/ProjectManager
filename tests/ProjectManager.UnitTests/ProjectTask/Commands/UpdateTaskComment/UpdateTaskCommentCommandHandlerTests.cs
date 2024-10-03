using Microsoft.Extensions.Logging;
using Moq;
using ProjectManager.Application.ProjectTask.Commands.UpdateTaskComment;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ProjectManager.UnitTests.ProjectTask.Commands.UpdateTaskComment
{
    public class UpdateTaskCommentCommandHandlerTests
    {
        private MockRepository mockRepository;

        private Mock<ICommentTaskRepository> mockCommentTaskRepository;
        private Mock<ILogger<UpdateTaskCommentCommandHandler>> mockLogger;
        private Mock<ITaskHistoryRepository> mockTaskHistoryRepository;

        public UpdateTaskCommentCommandHandlerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockCommentTaskRepository = this.mockRepository.Create<ICommentTaskRepository>();
            this.mockLogger = this.mockRepository.Create<ILogger<UpdateTaskCommentCommandHandler>>();
            this.mockTaskHistoryRepository = this.mockRepository.Create<ITaskHistoryRepository>();
        }

        private UpdateTaskCommentCommandHandler CreateUpdateTaskCommentCommandHandler()
        {
            return new UpdateTaskCommentCommandHandler(
                this.mockCommentTaskRepository.Object,
                this.mockLogger.Object,
                this.mockTaskHistoryRepository.Object);
        }

        [Fact]
        public async Task Handle_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var updateTaskCommentCommandHandler = this.CreateUpdateTaskCommentCommandHandler();
            UpdateTaskCommentCommand request = null;
            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

            // Act
            var result = await updateTaskCommentCommandHandler.Handle(
                request,
                cancellationToken);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}
