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
        //faça o setup seguindo o exemplo dos outros testes usando ProjectManagerFixture no request

        var projectManagerFixture = new Fixture().Customize(new AutoMoqCustomization());
        var getAllProjectsFromUserQueryHandler = CreateGetAllProjectsFromUserQueryHandler();

        var request = projectManagerFixture.Create<GetAllProjectsFromUserQuery>();
        CancellationToken cancellationToken = default;

        //setup mocks
        mockProjectRepository.Setup(x => x.GetAllProjectsFromUserAsync(It.IsAny<int>()))
            .ReturnsAsync(projectManagerFixture.CreateMany<Domain.Entities.Project>());

        // Act
        var result = await getAllProjectsFromUserQueryHandler.Handle(
            request,
            cancellationToken);

        // Assert
        result.Should().NotBeNull();
        result.Data.Projects.Count().Should().Be(3);
        result.Errors.Should().BeNullOrEmpty();
    }
}