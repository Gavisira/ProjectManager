﻿using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using ProjectManager.Application.ProjectTaskHistory.Queries.GetAllTaskHistoryById;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

namespace ProjectManager.UnitTests.ProjectTaskHistory.Queries.GetAllTaskHistoryById;

[TestClass]
public class GetAllTaskHistoryByIdQueryHandlerTests
{
    private readonly Mock<ILogger<GetAllTaskHistoryByIdQueryHandler>> mockLogger;
    private readonly MockRepository mockRepository;

    private readonly Mock<ITaskHistoryRepository> mockTaskHistoryRepository;

    public GetAllTaskHistoryByIdQueryHandlerTests()
    {
        mockRepository = new MockRepository(MockBehavior.Default);

        mockTaskHistoryRepository = mockRepository.Create<ITaskHistoryRepository>();
        mockLogger = mockRepository.Create<ILogger<GetAllTaskHistoryByIdQueryHandler>>();
    }

    private GetAllTaskHistoryByIdQueryHandler CreateGetAllTaskHistoryByIdQueryHandler()
    {
        return new GetAllTaskHistoryByIdQueryHandler(
            mockTaskHistoryRepository.Object,
            mockLogger.Object);
    }

    [Fact]
    public async Task Test_Success_Scenario()
    {
        var projectManagerFixture = new Fixture().Customize(new AutoMoqCustomization());
        projectManagerFixture.Behaviors.Add(new OmitOnRecursionBehavior());
        var getAllTaskHistoryByIdQueryHandler = CreateGetAllTaskHistoryByIdQueryHandler();
        var request = projectManagerFixture.Create<GetAllTaskHistoryByIdQuery>();
        CancellationToken cancellationToken = default;


        mockTaskHistoryRepository.Setup(x => x.GetAllTaskHistoryByTaskId(It.IsAny<int>()))
            .ReturnsAsync(projectManagerFixture.CreateMany<Domain.Entities.ProjectTaskHistory>().ToList());


        var result = await getAllTaskHistoryByIdQueryHandler.Handle(
            request,
            cancellationToken);


        result.Should().NotBeNull();
        result.Data.TaskHistory.Count().Should().Be(3);
        result.Errors.Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task Test_Failure_Scenario()
    {
        var projectManagerFixture = new Fixture().Customize(new AutoMoqCustomization());
        projectManagerFixture.Behaviors.Add(new OmitOnRecursionBehavior());
        var getAllTaskHistoryByIdQueryHandler = CreateGetAllTaskHistoryByIdQueryHandler();
        var request = projectManagerFixture.Create<GetAllTaskHistoryByIdQuery>();
        CancellationToken cancellationToken = default;

        mockTaskHistoryRepository.Setup(x => x.GetAllTaskHistoryByTaskId(It.IsAny<int>()))
            .ReturnsAsync((List<Domain.Entities.ProjectTaskHistory>)null);

        var result = await getAllTaskHistoryByIdQueryHandler.Handle(
            request,
            cancellationToken);

        result.Should().NotBeNull();
        result.Data.Should().BeNull();
        result.Errors.Should().NotBeNullOrEmpty();
        result.IsSuccess.Should().BeFalse();
    }
}