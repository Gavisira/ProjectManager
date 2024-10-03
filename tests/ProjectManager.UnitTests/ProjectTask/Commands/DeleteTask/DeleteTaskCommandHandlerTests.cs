using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using ProjectManager.Application.ProjectTask.Commands.DeleteTask;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

namespace ProjectManager.UnitTests.ProjectTask.Commands.DeleteTask;

[TestClass]
public class DeleteTaskCommandHandlerTests
{
    private readonly Mock<ILogger<DeleteTaskCommandHandler>> mockLogger;
    private readonly MockRepository mockRepository;
    private readonly Mock<ITaskHistoryRepository> mockTaskHistoryRepository;

    private readonly Mock<ITaskRepository> mockTaskRepository;

    public DeleteTaskCommandHandlerTests()
    {
        mockRepository = new MockRepository(MockBehavior.Default);

        mockTaskRepository = mockRepository.Create<ITaskRepository>();
        mockLogger = mockRepository.Create<ILogger<DeleteTaskCommandHandler>>();
        mockTaskHistoryRepository = mockRepository.Create<ITaskHistoryRepository>();
    }

    private DeleteTaskCommandHandler CreateDeleteTaskCommandHandler()
    {
        return new DeleteTaskCommandHandler(
            mockTaskRepository.Object,
            mockLogger.Object,
            mockTaskHistoryRepository.Object);
    }

    [Fact]
    public async Task Test_Success_Scenario()
    {
        //faça o setup seguindo o exemplo dos outros testes usando ProjectManagerFixture

        var projectManagerFixture = new Fixture().Customize(new AutoMoqCustomization());
        var deleteTaskCommandHandler = CreateDeleteTaskCommandHandler();
        var request = projectManagerFixture.Create<DeleteTaskCommand>();
        CancellationToken cancellationToken = default;

        //setup mocks
        mockTaskRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(projectManagerFixture.Create<Domain.Entities.ProjectTask>());
        mockTaskRepository.Setup(x => x.DeleteAsync(It.IsAny<int>())).ReturnsAsync(true);
        mockTaskHistoryRepository.Setup(x => x.AddAsync(It.IsAny<Domain.Entities.ProjectTaskHistory>()))
            .ReturnsAsync(projectManagerFixture.Create<Domain.Entities.ProjectTaskHistory>());

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