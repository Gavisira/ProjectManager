using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using ProjectManager.Application.ProjectTask.Commands.AddCommentToTask;
using ProjectManager.Domain.Entities;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

namespace ProjectManager.UnitTests.ProjectTask.Commands.AddCommentToTask;

[TestClass]
public class AddCommentToTaskCommandHandlerTests
{
    private readonly Mock<ICommentTaskRepository> mockCommentTaskRepository;
    private readonly Mock<ILogger<AddCommentToTaskCommandHandler>> mockLogger;
    private readonly MockRepository mockRepository;
    private readonly Mock<ITaskHistoryRepository> mockTaskHistoryRepository;

    public AddCommentToTaskCommandHandlerTests()
    {
        mockRepository = new MockRepository(MockBehavior.Default);

        mockCommentTaskRepository = mockRepository.Create<ICommentTaskRepository>();
        mockLogger = mockRepository.Create<ILogger<AddCommentToTaskCommandHandler>>();
        mockTaskHistoryRepository = mockRepository.Create<ITaskHistoryRepository>();
    }

    private AddCommentToTaskCommandHandler CreateAddCommentToTaskCommandHandler()
    {
        return new AddCommentToTaskCommandHandler(
            mockCommentTaskRepository.Object,
            mockLogger.Object,
            mockTaskHistoryRepository.Object);
    }

    [Fact]
    public async Task Test_Success_Scenario()
    {
        var projectManagerFixture = new Fixture().Customize(new AutoMoqCustomization());
        var addCommentToTaskCommandHandler = CreateAddCommentToTaskCommandHandler();
        var request = projectManagerFixture.Create<AddCommentToTaskCommand>();
        CancellationToken cancellationToken = default;


        mockCommentTaskRepository.Setup(x => x.AddAsync(It.IsAny<ProjectTaskComment>()))
            .ReturnsAsync(projectManagerFixture.Create<ProjectTaskComment>());


        var result = await addCommentToTaskCommandHandler.Handle(
            request,
            cancellationToken);


        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Errors.Should().BeNullOrEmpty();
    }
}