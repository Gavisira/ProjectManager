using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using ProjectManager.Application.Project.Queries.GetAllProjectsFromUser;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

namespace ProjectManager.UnitTests.Project.Queries.GetAllProjectsFromUser;

[TestClass]
public class GetAllProjectsFromUserQueryHandlerTests
{
    private readonly Mock<ILogger<GetAllProjectsFromUserQueryHandler>> mockLogger;

    private readonly Mock<IProjectRepository> mockProjectRepository;
    private readonly MockRepository mockRepository;

    public GetAllProjectsFromUserQueryHandlerTests()
    {
        mockRepository = new MockRepository(MockBehavior.Default);

        mockProjectRepository = mockRepository.Create<IProjectRepository>();
        mockLogger = mockRepository.Create<ILogger<GetAllProjectsFromUserQueryHandler>>();
    }

    private GetAllProjectsFromUserQueryHandler CreateGetAllProjectsFromUserQueryHandler()
    {
        return new GetAllProjectsFromUserQueryHandler(
            mockProjectRepository.Object,
            mockLogger.Object);
    }

    [Fact]
    public async Task Test_Success_Scenario()
    {
        var projectManagerFixture = new Fixture().Customize(new AutoMoqCustomization());
        projectManagerFixture.Behaviors.Add(new OmitOnRecursionBehavior());
        var getAllProjectsFromUserQueryHandler = CreateGetAllProjectsFromUserQueryHandler();

        var request = projectManagerFixture.Create<GetAllProjectsFromUserQuery>();
        CancellationToken cancellationToken = default;


        mockProjectRepository.Setup(x => x.GetAllProjectsFromUserAsync(It.IsAny<int>()))
            .ReturnsAsync(projectManagerFixture.CreateMany<Domain.Entities.Project>());


        var result = await getAllProjectsFromUserQueryHandler.Handle(
            request,
            cancellationToken);

        result.Should().NotBeNull();
        result.Data.Projects.Count().Should().Be(3);
        result.Errors.Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task Test_Failure_Scenario()
    {
        var projectManagerFixture = new Fixture().Customize(new AutoMoqCustomization());
        projectManagerFixture.Behaviors.Add(new OmitOnRecursionBehavior());
        var getAllProjectsFromUserQueryHandler = CreateGetAllProjectsFromUserQueryHandler();
        var request = projectManagerFixture.Create<GetAllProjectsFromUserQuery>();
        CancellationToken cancellationToken = default;

        mockProjectRepository.Setup(x => x.GetAllProjectsFromUserAsync(It.IsAny<int>()))
            .ThrowsAsync(new Exception());

        var result = await getAllProjectsFromUserQueryHandler.Handle(
            request,
            cancellationToken);

        result.Should().NotBeNull();
        result.Data.Should().BeNull();
        result.Errors.Should().NotBeNullOrEmpty();
        result.IsSuccess.Should().BeFalse();
    }


}