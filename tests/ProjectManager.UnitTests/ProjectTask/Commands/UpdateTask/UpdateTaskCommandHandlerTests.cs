using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using ProjectManager.Application.ProjectTask.Commands.UpdateTask;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

namespace ProjectManager.UnitTests.ProjectTask.Commands.UpdateTask;

[TestClass]
public class UpdateTaskCommandHandlerTests
{
    private readonly Mock<ILogger<UpdateTaskCommandHandler>> mockLogger;
    private readonly MockRepository mockRepository;
    private readonly Mock<ITaskHistoryRepository> mockTaskHistoryRepository;

    private readonly Mock<ITaskRepository> mockTaskRepository;

    public UpdateTaskCommandHandlerTests()
    {
        mockRepository = new MockRepository(MockBehavior.Default);

        mockTaskRepository = mockRepository.Create<ITaskRepository>();
        mockLogger = mockRepository.Create<ILogger<UpdateTaskCommandHandler>>();
        mockTaskHistoryRepository = mockRepository.Create<ITaskHistoryRepository>();
    }

    private UpdateTaskCommandHandler CreateUpdateTaskCommandHandler()
    {
        return new UpdateTaskCommandHandler(
            mockTaskRepository.Object,
            mockLogger.Object,
            mockTaskHistoryRepository.Object);
    }

    [Fact]
    public async Task Test_Success_Scenario()
    {
        var projectManagerFixture = new Fixture().Customize(new AutoMoqCustomization());
        var updateTaskCommandHandler = CreateUpdateTaskCommandHandler();
        var request = projectManagerFixture.Create<UpdateTaskCommand>();
        CancellationToken cancellationToken = default;


        mockTaskRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(projectManagerFixture.Create<Domain.Entities.ProjectTask>());
        mockTaskRepository.Setup(x => x.UpdateAsync(It.IsAny<Domain.Entities.ProjectTask>()))
            .ReturnsAsync(projectManagerFixture.Create<Domain.Entities.ProjectTask>());
        mockTaskHistoryRepository.Setup(x => x.AddAsync(It.IsAny<Domain.Entities.ProjectTaskHistory>()))
            .ReturnsAsync(projectManagerFixture.Create<Domain.Entities.ProjectTaskHistory>());


        var result = await updateTaskCommandHandler.Handle(
            request,
            cancellationToken);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Errors.Should().BeNullOrEmpty();
    }
}