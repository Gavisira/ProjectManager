using Microsoft.Extensions.Logging;
using Moq;
using ProjectManager.Application.ProjectTask.Commands.DeleteTaskComment;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ProjectManager.UnitTests.ProjectTask.Commands.DeleteTaskComment
{
    public class DeleteTaskCommentCommandHandlerTests
    {
        private MockRepository mockRepository;

        private Mock<ICommentTaskRepository> mockCommentTaskRepository;
        private Mock<ILogger<DeleteTaskCommentCommandHandler>> mockLogger;
        private Mock<ITaskHistoryRepository> mockTaskHistoryRepository;

        public DeleteTaskCommentCommandHandlerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockCommentTaskRepository = this.mockRepository.Create<ICommentTaskRepository>();
            this.mockLogger = this.mockRepository.Create<ILogger<DeleteTaskCommentCommandHandler>>();
            this.mockTaskHistoryRepository = this.mockRepository.Create<ITaskHistoryRepository>();
        }

        private DeleteTaskCommentCommandHandler CreateDeleteTaskCommentCommandHandler()
        {
            return new DeleteTaskCommentCommandHandler(
                this.mockCommentTaskRepository.Object,
                this.mockLogger.Object,
                this.mockTaskHistoryRepository.Object);
        }

        [Fact]
        public async Task Handle_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var deleteTaskCommentCommandHandler = this.CreateDeleteTaskCommentCommandHandler();
            DeleteTaskCommentCommand request = null;
            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

            // Act
            var result = await deleteTaskCommentCommandHandler.Handle(
                request,
                cancellationToken);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}
