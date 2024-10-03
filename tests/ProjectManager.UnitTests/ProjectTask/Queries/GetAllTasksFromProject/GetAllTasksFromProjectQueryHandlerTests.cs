using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using ProjectManager.Application.ProjectTask.Queries.GetAllTasksFromProject;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

namespace ProjectManager.UnitTests.ProjectTask.Queries.GetAllTasksFromProject;

[TestClass]
public class GetAllTasksFromProjectQueryHandlerTests
{
    private readonly Mock<ILogger<GetAllTasksFromProjectQueryHandler>> mockLogger;

    private readonly Mock<IProjectRepository> mockProjectRepository;
    private readonly MockRepository mockRepository;

    public GetAllTasksFromProjectQueryHandlerTests()
    {
        mockRepository = new MockRepository(MockBehavior.Default);

        mockProjectRepository = mockRepository.Create<IProjectRepository>();
        mockLogger = mockRepository.Create<ILogger<GetAllTasksFromProjectQueryHandler>>();
    }

    private GetAllTasksFromProjectQueryHandler CreateGetAllTasksFromProjectQueryHandler()
    {
        return new GetAllTasksFromProjectQueryHandler(
            mockProjectRepository.Object,
            mockLogger.Object);
    }

    [Fact]
    public async Task Test_Success_Scenario()
    {
        var projectManagerFixture = new Fixture().Customize(new AutoMoqCustomization());
        var getAllTasksFromProjectQueryHandler = CreateGetAllTasksFromProjectQueryHandler();
        var request = projectManagerFixture.Create<GetAllTasksFromProjectQuery>();
        CancellationToken cancellationToken = default;


        mockProjectRepository.Setup(x => x.GetByIdAsNoTrackingAsync(It.IsAny<int>()))
            .ReturnsAsync(projectManagerFixture.Create<Domain.Entities.Project>());


        var result = await getAllTasksFromProjectQueryHandler.Handle(
            request,
            cancellationToken);

        result.Should().NotBeNull();
        result.Data.Tasks.Count().Should().Be(3);
        result.Errors.Should().BeNullOrEmpty();
    }
}