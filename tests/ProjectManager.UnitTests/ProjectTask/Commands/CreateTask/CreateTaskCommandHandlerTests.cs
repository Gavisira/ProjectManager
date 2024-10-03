using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using ProjectManager.Application.ProjectTask.Commands.CreateTask;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

namespace ProjectManager.UnitTests.ProjectTask.Commands.CreateTask;

[TestClass]
public class CreateTaskCommandHandlerTests
{
    private readonly Mock<ILogger<CreateTaskCommandHandler>> mockLogger;
    private readonly MockRepository mockRepository;
    private readonly Mock<ITaskHistoryRepository> mockTaskHistoryRepository;

    private readonly Mock<ITaskRepository> mockTaskRepository;

    public CreateTaskCommandHandlerTests()
    {
        mockRepository = new MockRepository(MockBehavior.Default);

        mockTaskRepository = mockRepository.Create<ITaskRepository>();
        mockLogger = mockRepository.Create<ILogger<CreateTaskCommandHandler>>();
        mockTaskHistoryRepository = mockRepository.Create<ITaskHistoryRepository>();
    }

    private CreateTaskCommandHandler CreateCreateTaskCommandHandler()
    {
        return new CreateTaskCommandHandler(
            mockTaskRepository.Object,
            mockLogger.Object,
            mockTaskHistoryRepository.Object);
    }

    [Fact]
    public async Task Test_Success_Scenario()
    {
        //faça o setup seguindo o exemplo dos outros testes usando ProjectManagerFixture

        var projectManagerFixture = new Fixture().Customize(new AutoMoqCustomization());
        var createTaskCommandHandler = CreateCreateTaskCommandHandler();
        var request = projectManagerFixture.Create<CreateTaskCommand>();
        CancellationToken cancellationToken = default;

        //setup mocks
        mockTaskRepository.Setup(x => x.AddAsync(It.IsAny<Domain.Entities.ProjectTask>()))
            .ReturnsAsync(projectManagerFixture.Create<Domain.Entities.ProjectTask>());
        mockTaskHistoryRepository.Setup(x => x.AddAsync(It.IsAny<Domain.Entities.ProjectTaskHistory>()))
            .ReturnsAsync(projectManagerFixture.Create<Domain.Entities.ProjectTaskHistory>());

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