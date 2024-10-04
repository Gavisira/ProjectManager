using Microsoft.Extensions.Logging;
using Moq;
using ProjectManager.Application.ProjectTask.Commands.UpdateTaskComment;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;

namespace ProjectManager.UnitTests.ProjectTask.Commands.UpdateTaskComment
{
    [TestClass] public class UpdateTaskCommentCommandHandlerTests : DefaultFixture
    {
        private MockRepository mockRepository;

        private Mock<ICommentTaskRepository> mockCommentTaskRepository;
        private Mock<ILogger<UpdateTaskCommentCommandHandler>> mockLogger;
        private Mock<ITaskHistoryRepository> mockTaskHistoryRepository;

        public UpdateTaskCommentCommandHandlerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Default);

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
        public async Task Test_Success_Scenario()
        {
            //faça o setup seguindo o exemplo dos outros testes usando ProjectManagerFixture

            // Arrange
            var updateTaskCommentCommandHandler = this.CreateUpdateTaskCommentCommandHandler();
            UpdateTaskCommentCommand request = ProjectManagerFixture.Create<UpdateTaskCommentCommand>();
            CancellationToken cancellationToken = default;

            //setup mocks
            mockCommentTaskRepository.Setup(x => x.UpdateAsync(It.IsAny<Domain.Entities.ProjectTaskComment>())).ReturnsAsync(ProjectManagerFixture.Create<Domain.Entities.ProjectTaskComment>());
            mockTaskHistoryRepository.Setup(x => x.AddAsync(It.IsAny<Domain.Entities.ProjectTaskHistory>())).ReturnsAsync(ProjectManagerFixture.Create<Domain.Entities.ProjectTaskHistory>());

            // Act
            var result = await updateTaskCommentCommandHandler.Handle(
                request,
                cancellationToken);


            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();



        }
    }
}
