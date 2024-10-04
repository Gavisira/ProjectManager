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
        var projectManagerFixture = new Fixture().Customize(new AutoMoqCustomization());
        projectManagerFixture.Behaviors.Add(new OmitOnRecursionBehavior());
        var deleteProjectCommandHandler = CreateDeleteProjectCommandHandler();
        var request = projectManagerFixture.Create<DeleteProjectCommand>();
        CancellationToken cancellationToken = default;

        var projectReturn = projectManagerFixture.Create<Domain.Entities.Project>();
        projectReturn.Tasks = new List<Domain.Entities.ProjectTask>();


        mockProjectRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(projectReturn);
        mockProjectRepository.Setup(x => x.DeleteAsync(It.IsAny<int>())).ReturnsAsync(true);


        var result = await deleteProjectCommandHandler.Handle(
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
        var deleteProjectCommandHandler = CreateDeleteProjectCommandHandler();
        var request = projectManagerFixture.Create<DeleteProjectCommand>();
        CancellationToken cancellationToken = default;
        var projectReturn = projectManagerFixture.Create<Domain.Entities.Project>();
        projectReturn.Tasks = new List<Domain.Entities.ProjectTask>();

        mockProjectRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(projectReturn);
        mockProjectRepository.Setup(x => x.DeleteAsync(It.IsAny<int>()))
            .ThrowsAsync(new Exception("Error deleting project"));
        var result = await deleteProjectCommandHandler.Handle(
            request,
            cancellationToken);
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().NotBeNullOrEmpty();
    }
}