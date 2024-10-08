﻿using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using ProjectManager.Application;
using ProjectManager.Application.ProjectTask.Queries.GetPerformanceReport;
using ProjectManager.Domain.Entities;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

namespace ProjectManager.UnitTests.ProjectTask.Queries.GetPerformanceReport;

public class GetPerformanceReportQueryHandlerTests
{
    private readonly Mock<ILogger<GetPerformanceReportQueryHandler>> mockLogger;

    private readonly Mock<IProjectRepository> mockProjectRepository;
    private readonly MockRepository mockRepository;
    private readonly Mock<ITaskHistoryRepository> mockTaskHistoryRepository;
    private readonly Mock<ITaskRepository> mockTaskRepository;
    private readonly Mock<IUserRepository> mockUserRepository;

    public GetPerformanceReportQueryHandlerTests()
    {
        mockRepository = new MockRepository(MockBehavior.Default);

        mockProjectRepository = mockRepository.Create<IProjectRepository>();
        mockLogger = mockRepository.Create<ILogger<GetPerformanceReportQueryHandler>>();
        mockTaskRepository = mockRepository.Create<ITaskRepository>();
        mockUserRepository = mockRepository.Create<IUserRepository>();
        mockTaskHistoryRepository = mockRepository.Create<ITaskHistoryRepository>();
    }

    private GetPerformanceReportQueryHandler CreateGetPerformanceReportQueryHandler()
    {
        return new GetPerformanceReportQueryHandler(
            mockProjectRepository.Object,
            mockLogger.Object,
            mockTaskRepository.Object,
            mockUserRepository.Object,
            mockTaskHistoryRepository.Object);
    }

    [Fact]
    public async Task Test_Success_Scenario()
    {
        var projectManagerFixture = new Fixture().Customize(new AutoMoqCustomization());
        projectManagerFixture.Behaviors.Add(new OmitOnRecursionBehavior());
        var getPerformanceReportQueryHandler = CreateGetPerformanceReportQueryHandler();
        var request = projectManagerFixture.Create<GetPerformanceReportQuery>();
        CancellationToken cancellationToken = default;
        mockUserRepository.Setup(x => x.GetByIdAsNoTrackingAsync(It.IsAny<int>()))
            .ReturnsAsync(projectManagerFixture.Create<User>());
        mockUserRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(projectManagerFixture.Create<User>());
        mockTaskRepository.Setup(x => x.GetAllTasksByUser(It.IsAny<int>()))
            .ReturnsAsync(projectManagerFixture.CreateMany<Domain.Entities.ProjectTask>(3));


        var result = await getPerformanceReportQueryHandler.Handle(
            request,
            cancellationToken);


        result.Should().NotBeNull();
        result.Should().BeOfType<BaseResponse<GetPerformanceReportQueryResponse>>();
        result.Data.Should().NotBeNull();
        result.Data.Should().BeOfType<GetPerformanceReportQueryResponse>();
        result.IsSuccess.Should().BeTrue();
        result.Errors.Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task Test_Exception_Scenario()
    {
        var projectManagerFixture = new Fixture().Customize(new AutoMoqCustomization());
        projectManagerFixture.Behaviors.Add(new OmitOnRecursionBehavior());
        var getPerformanceReportQueryHandler = CreateGetPerformanceReportQueryHandler();
        var request = projectManagerFixture.Create<GetPerformanceReportQuery>();
        CancellationToken cancellationToken = default;
        mockUserRepository.Setup(x => x.GetByIdAsNoTrackingAsync(It.IsAny<int>()))
            .Throws(new Exception("Error"));
        var result = await getPerformanceReportQueryHandler.Handle(
            request,
            cancellationToken);
        result.Should().NotBeNull();
        result.Should().BeOfType<BaseResponse<GetPerformanceReportQueryResponse>>();
        result.Data.Should().BeNull();
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().NotBeNullOrEmpty();
    }

}