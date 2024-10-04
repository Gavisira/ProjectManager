using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using ProjectManager.Application.Project.Commands.CreateProject;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

namespace ProjectManager.UnitTests.Project.Commands.CreateProject;

[TestClass]
public class CreateProjectCommandHandlerTests
{
    private readonly Mock<ILogger<CreateProjectCommandHandler>> mockLogger;

    private readonly Mock<IProjectRepository> mockProjectRepository;
    private readonly MockRepository mockRepository;

    public CreateProjectCommandHandlerTests()
    {
        mockRepository = new MockRepository(MockBehavior.Default);

        mockProjectRepository = mockRepository.Create<IProjectRepository>();
        mockLogger = mockRepository.Create<ILogger<CreateProjectCommandHandler>>();
    }

    private CreateProjectCommandHandler CreateCreateProjectCommandHandler()
    {
        return new CreateProjectCommandHandler(
            mockProjectRepository.Object,
            mockLogger.Object);
    }

    [Fact]
    public async Task Test_Success_Scenario()
    {
        var projectManagerFixture = new Fixture().Customize(new AutoMoqCustomization());
        projectManagerFixture.Behaviors.Add(new OmitOnRecursionBehavior());
        var createProjectCommandHandler = CreateCreateProjectCommandHandler();
        var request = projectManagerFixture.Create<CreateProjectCommand>();
        CancellationToken cancellationToken = default;


        mockProjectRepository.Setup(x => x.AddAsync(It.IsAny<Domain.Entities.Project>()))
            .ReturnsAsync(projectManagerFixture.Create<Domain.Entities.Project>());


        var result = await createProjectCommandHandler.Handle(
            request,
            cancellationToken);


        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Errors.Should().BeNullOrEmpty();

        mockRepository.VerifyAll();
    }

    [Fact]
    public async Task Test_Exception_Scenario()
    {
        var projectManagerFixture = new Fixture().Customize(new AutoMoqCustomization());
        projectManagerFixture.Behaviors.Add(new OmitOnRecursionBehavior());
        var createProjectCommandHandler = CreateCreateProjectCommandHandler();
        var request = projectManagerFixture.Create<CreateProjectCommand>();
        CancellationToken cancellationToken = default;
        mockProjectRepository.Setup(x => x.AddAsync(It.IsAny<Domain.Entities.Project>()))
            .ThrowsAsync(new Exception("Error adding project"));
        var result = await createProjectCommandHandler.Handle(
            request,
            cancellationToken);
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().NotBeNullOrEmpty();
        mockRepository.VerifyAll();
    }

}