using Microsoft.Extensions.Logging;
using Moq;
using ProjectManager.Application.ProjectTaskHistory.Queries.GetAllTaskHistoryById;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ProjectManager.UnitTests.ProjectTaskHistory.Queries.GetAllTaskHistoryById
{
    public class GetAllTaskHistoryByIdQueryHandlerTests
    {
        private MockRepository mockRepository;

        private Mock<ITaskHistoryRepository> mockTaskHistoryRepository;
        private Mock<ILogger<GetAllTaskHistoryByIdQueryHandler>> mockLogger;

        public GetAllTaskHistoryByIdQueryHandlerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

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
        public async Task Handle_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var getAllTaskHistoryByIdQueryHandler = this.CreateGetAllTaskHistoryByIdQueryHandler();
            GetAllTaskHistoryByIdQuery request = null;
            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

            // Act
            var result = await getAllTaskHistoryByIdQueryHandler.Handle(
                request,
                cancellationToken);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}
