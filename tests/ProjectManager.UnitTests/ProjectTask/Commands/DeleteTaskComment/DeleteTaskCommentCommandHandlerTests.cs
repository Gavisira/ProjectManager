using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using ProjectManager.Application.ProjectTask.Commands.DeleteTaskComment;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

namespace ProjectManager.UnitTests.ProjectTask.Commands.DeleteTaskComment;

[TestClass]
public class DeleteTaskCommentCommandHandlerTests
{
    private readonly Mock<ICommentTaskRepository> mockCommentTaskRepository;
    private readonly Mock<ILogger<DeleteTaskCommentCommandHandler>> mockLogger;
    private readonly MockRepository mockRepository;
    private readonly Mock<ITaskHistoryRepository> mockTaskHistoryRepository;

    public DeleteTaskCommentCommandHandlerTests()
    {
        mockRepository = new MockRepository(MockBehavior.Default);

        mockCommentTaskRepository = mockRepository.Create<ICommentTaskRepository>();
        mockLogger = mockRepository.Create<ILogger<DeleteTaskCommentCommandHandler>>();
        mockTaskHistoryRepository = mockRepository.Create<ITaskHistoryRepository>();
    }

    private DeleteTaskCommentCommandHandler CreateDeleteTaskCommentCommandHandler()
    {
        return new DeleteTaskCommentCommandHandler(
            mockCommentTaskRepository.Object,
            mockLogger.Object,
            mockTaskHistoryRepository.Object);
    }

    [Fact]
    public async Task Test_Success_Scenario()
    {
        var projectManagerFixture = new Fixture().Customize(new AutoMoqCustomization());
        projectManagerFixture.Behaviors.Add(new OmitOnRecursionBehavior());
        var deleteTaskCommentCommandHandler = CreateDeleteTaskCommentCommandHandler();
        var request = projectManagerFixture.Create<DeleteTaskCommentCommand>();
        CancellationToken cancellationToken = default;


        mockCommentTaskRepository.Setup(x => x.DeleteAsync(It.IsAny<int>())).ReturnsAsync(true);
        mockTaskHistoryRepository.Setup(x => x.AddAsync(It.IsAny<Domain.Entities.ProjectTaskHistory>()))
            .ReturnsAsync(projectManagerFixture.Create<Domain.Entities.ProjectTaskHistory>());
        mockCommentTaskRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(projectManagerFixture.Create<Domain.Entities.ProjectTaskComment>());


        var result = await deleteTaskCommentCommandHandler.Handle(
            request,
            cancellationToken);


        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Errors.Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task Test_Exception_Scenario()
    {
        var projectManagerFixture = new Fixture().Customize(new AutoMoqCustomization());
        projectManagerFixture.Behaviors.Add(new OmitOnRecursionBehavior());
        var deleteTaskCommentCommandHandler = CreateDeleteTaskCommentCommandHandler();
        var request = projectManagerFixture.Create<DeleteTaskCommentCommand>();
        CancellationToken cancellationToken = default;

        mockCommentTaskRepository.Setup(x => x.DeleteAsync(It.IsAny<int>())).Throws(new Exception("Error"));
        mockTaskHistoryRepository.Setup(x => x.AddAsync(It.IsAny<Domain.Entities.ProjectTaskHistory>()))
            .ReturnsAsync(projectManagerFixture.Create<Domain.Entities.ProjectTaskHistory>());
        mockCommentTaskRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(projectManagerFixture.Create<Domain.Entities.ProjectTaskComment>());

        var result = await deleteTaskCommentCommandHandler.Handle(
            request,
            cancellationToken);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().NotBeNullOrEmpty();

    }
}