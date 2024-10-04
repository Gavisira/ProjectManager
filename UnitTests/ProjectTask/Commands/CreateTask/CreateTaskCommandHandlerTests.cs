using Microsoft.Extensions.Logging;
using Moq;
using ProjectManager.Application.ProjectTask.Commands.CreateTask;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;

namespace ProjectManager.UnitTests.ProjectTask.Commands.CreateTask
{
    [TestClass] public class CreateTaskCommandHandlerTests : DefaultFixture
    {
        private MockRepository mockRepository;

        private Mock<ITaskRepository> mockTaskRepository;
        private Mock<ILogger<CreateTaskCommandHandler>> mockLogger;
        private Mock<ITaskHistoryRepository> mockTaskHistoryRepository;

        public CreateTaskCommandHandlerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Default);

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
        public async Task Test_Success_Scenario()
        {
            //faça o setup seguindo o exemplo dos outros testes usando ProjectManagerFixture

            // Arrange
            var createTaskCommandHandler = this.CreateCreateTaskCommandHandler();
            CreateTaskCommand request = ProjectManagerFixture.Create<CreateTaskCommand>();
            CancellationToken cancellationToken = default;

            //setup mocks
            mockTaskRepository.Setup(x => x.AddAsync(It.IsAny<Domain.Entities.ProjectTask>())).ReturnsAsync(ProjectManagerFixture.Create<Domain.Entities.ProjectTask>());
            mockTaskHistoryRepository.Setup(x => x.AddAsync(It.IsAny<Domain.Entities.ProjectTaskHistory>())).ReturnsAsync(ProjectManagerFixture.Create<Domain.Entities.ProjectTaskHistory>());

            // Act
            var result = await createTaskCommandHandler.Handle(
                request,
                cancellationToken);


            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();
        }
    }
}
