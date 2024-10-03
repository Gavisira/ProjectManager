using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using ProjectManager.Application.ProjectTask.Commands.UpdateTaskComment;
using ProjectManager.Domain.Entities;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

namespace ProjectManager.UnitTests.ProjectTask.Commands.UpdateTaskComment;

[TestClass]
public class UpdateTaskCommentCommandHandlerTests
{
    private readonly Mock<ICommentTaskRepository> mockCommentTaskRepository;
    private readonly Mock<ILogger<UpdateTaskCommentCommandHandler>> mockLogger;
    private readonly MockRepository mockRepository;
    private readonly Mock<ITaskHistoryRepository> mockTaskHistoryRepository;

    public UpdateTaskCommentCommandHandlerTests()
    {
        mockRepository = new MockRepository(MockBehavior.Default);

        mockCommentTaskRepository = mockRepository.Create<ICommentTaskRepository>();
        mockLogger = mockRepository.Create<ILogger<UpdateTaskCommentCommandHandler>>();
        mockTaskHistoryRepository = mockRepository.Create<ITaskHistoryRepository>();
    }

    private UpdateTaskCommentCommandHandler CreateUpdateTaskCommentCommandHandler()
    {
        return new UpdateTaskCommentCommandHandler(
            mockCommentTaskRepository.Object,
            mockLogger.Object,
            mockTaskHistoryRepository.Object);
    }

    [Fact]
    public async Task Test_Success_Scenario()
    {
        var projectManagerFixture = new Fixture().Customize(new AutoMoqCustomization());
        var updateTaskCommentCommandHandler = CreateUpdateTaskCommentCommandHandler();
        var request = projectManagerFixture.Create<UpdateTaskCommentCommand>();
        CancellationToken cancellationToken = default;


        mockCommentTaskRepository.Setup(x => x.UpdateAsync(It.IsAny<ProjectTaskComment>()))
            .ReturnsAsync(projectManagerFixture.Create<ProjectTaskComment>());
        mockTaskHistoryRepository.Setup(x => x.AddAsync(It.IsAny<Domain.Entities.ProjectTaskHistory>()))
            .ReturnsAsync(projectManagerFixture.Create<Domain.Entities.ProjectTaskHistory>());


        var result = await updateTaskCommentCommandHandler.Handle(
            request,
            cancellationToken);


        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Errors.Should().BeNullOrEmpty();
    }
}