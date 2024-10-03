using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using ProjectManager.Application.Project.Commands.UpdateProject;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

namespace ProjectManager.UnitTests.Project.Commands.UpdateProject;

[TestClass]
public class UpdateProjectCommandHandlerTests
{
    private readonly Mock<ILogger<UpdateProjectCommandHandler>> mockLogger;

    private readonly Mock<IProjectRepository> mockProjectRepository;
    private readonly MockRepository mockRepository;

    public UpdateProjectCommandHandlerTests()
    {
        mockRepository = new MockRepository(MockBehavior.Default);

        mockProjectRepository = mockRepository.Create<IProjectRepository>();
        mockLogger = mockRepository.Create<ILogger<UpdateProjectCommandHandler>>();
    }

    private UpdateProjectCommandHandler CreateUpdateProjectCommandHandler()
    {
        return new UpdateProjectCommandHandler(
            mockProjectRepository.Object,
            mockLogger.Object);
    }

    [Fact]
    public async Task Test_Success_Scenario()
    {
        //faça o setup seguindo o exemplo dos outros testes usando ProjectManagerFixture
        var projectManagerFixture = new Fixture().Customize(new AutoMoqCustomization());
        var updateProjectCommandHandler = CreateUpdateProjectCommandHandler();
        var request = projectManagerFixture.Create<UpdateProjectCommand>();
        CancellationToken cancellationToken = default;

        //setup mocks
        mockProjectRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(projectManagerFixture.Create<Domain.Entities.Project>());
        mockProjectRepository.Setup(x => x.UpdateAsync(It.IsAny<Domain.Entities.Project>()))
            .ReturnsAsync(projectManagerFixture.Create<Domain.Entities.Project>());

        // Act
        var result = await updateProjectCommandHandler.Handle(
            request,
            cancellationToken);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Errors.Should().BeNullOrEmpty();
    }
}