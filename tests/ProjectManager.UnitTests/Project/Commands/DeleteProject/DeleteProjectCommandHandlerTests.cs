using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using ProjectManager.Application.Project.Commands.DeleteProject;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

namespace ProjectManager.UnitTests.Project.Commands.DeleteProject;

[TestClass]
public class DeleteProjectCommandHandlerTests
{
    private readonly Mock<ILogger<DeleteProjectCommandHandler>> mockLogger;

    private readonly Mock<IProjectRepository> mockProjectRepository;
    private readonly MockRepository mockRepository;

    public DeleteProjectCommandHandlerTests()
    {
        mockRepository = new MockRepository(MockBehavior.Default);

        mockProjectRepository = mockRepository.Create<IProjectRepository>();
        mockLogger = mockRepository.Create<ILogger<DeleteProjectCommandHandler>>();
    }

    private DeleteProjectCommandHandler CreateDeleteProjectCommandHandler()
    {
        return new DeleteProjectCommandHandler(
            mockProjectRepository.Object,
            mockLogger.Object);
    }

    [Fact]
    public async Task Test_Success_Scenario()
    {
        //create tests using ProjectManagerFixture
        var projectManagerFixture = new Fixture().Customize(new AutoMoqCustomization());
        var deleteProjectCommandHandler = CreateDeleteProjectCommandHandler();
        var request = projectManagerFixture.Create<DeleteProjectCommand>();
        CancellationToken cancellationToken = default;

        //setup mocks
        mockProjectRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(projectManagerFixture.Create<Domain.Entities.Project>());
        mockProjectRepository.Setup(x => x.DeleteAsync(It.IsAny<int>())).ReturnsAsync(true);

        // Act
        var result = await deleteProjectCommandHandler.Handle(
            request,
            cancellationToken);

        // Assert with fluent assertions
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Errors.Should().BeNullOrEmpty();
    }
}