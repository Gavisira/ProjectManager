using Microsoft.Extensions.Logging;
using Moq;
using ProjectManager.Application.ProjectTask.Commands.DeleteTask;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;

namespace ProjectManager.UnitTests.ProjectTask.Commands.DeleteTask
{
    [TestClass] public class DeleteTaskCommandHandlerTests : DefaultFixture
    {
        private MockRepository mockRepository;

        private Mock<ITaskRepository> mockTaskRepository;
        private Mock<ILogger<DeleteTaskCommandHandler>> mockLogger;
        private Mock<ITaskHistoryRepository> mockTaskHistoryRepository;

        public DeleteTaskCommandHandlerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Default);

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
        public async Task Test_Success_Scenario()
        {
            //faça o setup seguindo o exemplo dos outros testes usando ProjectManagerFixture

            // Arrange
            var deleteTaskCommandHandler = this.CreateDeleteTaskCommandHandler();
            DeleteTaskCommand request = ProjectManagerFixture.Create<DeleteTaskCommand>();
            CancellationToken cancellationToken = default;

            //setup mocks
            mockTaskRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(ProjectManagerFixture.Create<Domain.Entities.ProjectTask>());
            mockTaskRepository.Setup(x => x.DeleteAsync(It.IsAny<int>())).ReturnsAsync(true);
            mockTaskHistoryRepository.Setup(x => x.AddAsync(It.IsAny<Domain.Entities.ProjectTaskHistory>())).ReturnsAsync(ProjectManagerFixture.Create<Domain.Entities.ProjectTaskHistory>());

            // Act
            var result = await deleteTaskCommandHandler.Handle(
                request,
                cancellationToken);


            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();

        }
    }
}
