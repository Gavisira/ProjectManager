using Microsoft.Extensions.Logging;
using Moq;
using ProjectManager.Application.ProjectTaskHistory.Queries.GetAllTaskHistoryById;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;

namespace ProjectManager.UnitTests.ProjectTaskHistory.Queries.GetAllTaskHistoryById
{
    [TestClass] public class GetAllTaskHistoryByIdQueryHandlerTests : DefaultFixture
    {
        private MockRepository mockRepository;

        private Mock<ITaskHistoryRepository> mockTaskHistoryRepository;
        private Mock<ILogger<GetAllTaskHistoryByIdQueryHandler>> mockLogger;

        public GetAllTaskHistoryByIdQueryHandlerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Default);

            this.mockTaskHistoryRepository = this.mockRepository.Create<ITaskHistoryRepository>();
            this.mockLogger = this.mockRepository.Create<ILogger<GetAllTaskHistoryByIdQueryHandler>>();
        }

        private GetAllTaskHistoryByIdQueryHandler CreateGetAllTaskHistoryByIdQueryHandler()
        {
            return new GetAllTaskHistoryByIdQueryHandler(
                this.mockTaskHistoryRepository.Object,
                this.mockLogger.Object);
        }

        [Fact]
        public async Task Test_Success_Scenario()
        {
            //faça o setup seguindo o exemplo dos outros testes usando ProjectManagerFixture

            // Arrange
            var getAllTaskHistoryByIdQueryHandler = this.CreateGetAllTaskHistoryByIdQueryHandler();
            var request = ProjectManagerFixture.Create<GetAllTaskHistoryByIdQuery>();
            CancellationToken cancellationToken = default;

            //setup mocks
            mockTaskHistoryRepository.Setup(x => x.GetAllTaskHistoryByTaskId(It.IsAny<int>())).ReturnsAsync((ProjectManagerFixture.CreateMany<Domain.Entities.ProjectTaskHistory>()).ToList());

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
}
