using Microsoft.Extensions.Logging;
using Moq;
using ProjectManager.Application.ProjectTask.Commands.UpdateTask;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;

namespace ProjectManager.UnitTests.ProjectTask.Commands.UpdateTask
{
    [TestClass] public class UpdateTaskCommandHandlerTests : DefaultFixture
    {
        private MockRepository mockRepository;

        private Mock<ITaskRepository> mockTaskRepository;
        private Mock<ILogger<UpdateTaskCommandHandler>> mockLogger;
        private Mock<ITaskHistoryRepository> mockTaskHistoryRepository;

        public UpdateTaskCommandHandlerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Default);

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
        public async Task Test_Success_Scenario()
        {
            //faça o setup seguindo o exemplo dos outros testes usando ProjectManagerFixture

            // Arrange
            var updateTaskCommandHandler = this.CreateUpdateTaskCommandHandler();
            UpdateTaskCommand request = ProjectManagerFixture.Create<UpdateTaskCommand>();
            CancellationToken cancellationToken = default;

            //setup mocks
            mockTaskRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(ProjectManagerFixture.Create<Domain.Entities.ProjectTask>());
            mockTaskRepository.Setup(x => x.UpdateAsync(It.IsAny<Domain.Entities.ProjectTask>())).ReturnsAsync(ProjectManagerFixture.Create<Domain.Entities.ProjectTask>());
            mockTaskHistoryRepository.Setup(x => x.AddAsync(It.IsAny<Domain.Entities.ProjectTaskHistory>())).ReturnsAsync(ProjectManagerFixture.Create<Domain.Entities.ProjectTaskHistory>());

            // Act
            var result = await updateTaskCommandHandler.Handle(
                request,
                cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();


        }
    }
}
