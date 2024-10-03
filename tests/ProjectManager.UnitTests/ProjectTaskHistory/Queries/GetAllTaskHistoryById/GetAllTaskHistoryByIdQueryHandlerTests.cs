using AutoFixture;
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
        //faça o setup seguindo o exemplo dos outros testes usando ProjectManagerFixture

        var projectManagerFixture = new Fixture().Customize(new AutoMoqCustomization());
        var getAllTaskHistoryByIdQueryHandler = CreateGetAllTaskHistoryByIdQueryHandler();
        var request = projectManagerFixture.Create<GetAllTaskHistoryByIdQuery>();
        CancellationToken cancellationToken = default;

        //setup mocks
        mockTaskHistoryRepository.Setup(x => x.GetAllTaskHistoryByTaskId(It.IsAny<int>()))
            .ReturnsAsync(projectManagerFixture.CreateMany<Domain.Entities.ProjectTaskHistory>().ToList());

        // Act
        var result = await getAllTaskHistoryByIdQueryHandler.Handle(
            request,
            cancellationToken);


        // Assert
        result.Should().NotBeNull();
        result.Data.TaskHistory.Count().Should().Be(3);
        result.Errors.Should().BeNullOrEmpty();
    }
}