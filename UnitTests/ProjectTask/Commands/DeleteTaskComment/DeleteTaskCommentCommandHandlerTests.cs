using Microsoft.Extensions.Logging;
using Moq;
using ProjectManager.Application.ProjectTask.Commands.DeleteTaskComment;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;

namespace ProjectManager.UnitTests.ProjectTask.Commands.DeleteTaskComment
{
    [TestClass] public class DeleteTaskCommentCommandHandlerTests : DefaultFixture
    {
        private MockRepository mockRepository;

        private Mock<ICommentTaskRepository> mockCommentTaskRepository;
        private Mock<ILogger<DeleteTaskCommentCommandHandler>> mockLogger;
        private Mock<ITaskHistoryRepository> mockTaskHistoryRepository;

        public DeleteTaskCommentCommandHandlerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Default);

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
        public async Task Test_Success_Scenario()
        {
            //faça o setup seguindo o exemplo dos outros testes usando ProjectManagerFixture

            // Arrange
            var deleteTaskCommentCommandHandler = this.CreateDeleteTaskCommentCommandHandler();
            DeleteTaskCommentCommand request = ProjectManagerFixture.Create<DeleteTaskCommentCommand>();
            CancellationToken cancellationToken = default;

            //setup mocks
            mockCommentTaskRepository.Setup(x => x.DeleteAsync(It.IsAny<int>())).ReturnsAsync(true);
            mockTaskHistoryRepository.Setup(x => x.AddAsync(It.IsAny<Domain.Entities.ProjectTaskHistory>())).ReturnsAsync(ProjectManagerFixture.Create<Domain.Entities.ProjectTaskHistory>());

            // Act
            var result = await deleteTaskCommentCommandHandler.Handle(
                request,
                cancellationToken);


            //assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();

        }
    }
}
