using Microsoft.Extensions.Logging;
using Moq;
using ProjectManager.Application.ProjectTask.Commands.AddCommentToTask;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;

namespace ProjectManager.UnitTests.ProjectTask.Commands.AddCommentToTask
{
    [TestClass] public class AddCommentToTaskCommandHandlerTests : DefaultFixture
    {
        private MockRepository mockRepository;

        private Mock<ICommentTaskRepository> mockCommentTaskRepository;
        private Mock<ILogger<AddCommentToTaskCommandHandler>> mockLogger;
        private Mock<ITaskHistoryRepository> mockTaskHistoryRepository;

        public AddCommentToTaskCommandHandlerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Default);

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
        public async Task Test_Success_Scenario()
        {
            //faça o setup seguindo o exemplo dos outros testes usando ProjectManagerFixture

            // Arrange
            var addCommentToTaskCommandHandler = this.CreateAddCommentToTaskCommandHandler();
            AddCommentToTaskCommand request = ProjectManagerFixture.Create<AddCommentToTaskCommand>();
            CancellationToken cancellationToken = default;

            //setup mocks
            mockCommentTaskRepository.Setup(x => x.AddAsync(It.IsAny<Domain.Entities.ProjectTaskComment>())).ReturnsAsync(ProjectManagerFixture.Create<Domain.Entities.ProjectTaskComment>());

            // Act
            var result = await addCommentToTaskCommandHandler.Handle(
                request,
                cancellationToken);


            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();



        }
    }
}
