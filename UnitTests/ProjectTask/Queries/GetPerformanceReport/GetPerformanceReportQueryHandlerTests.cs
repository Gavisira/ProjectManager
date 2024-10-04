using Microsoft.Extensions.Logging;
using Moq;
using ProjectManager.Application.ProjectTask.Queries.GetPerformanceReport;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManager.Application;
using ProjectManager.Domain.Entities;
using Xunit;

namespace ProjectManager.UnitTests.ProjectTask.Queries.GetPerformanceReport
{
    [TestClass] public class GetPerformanceReportQueryHandlerTests : DefaultFixture
    {
        private MockRepository mockRepository;

        private Mock<IProjectRepository> mockProjectRepository;
        private Mock<ILogger<GetPerformanceReportQueryHandler>> mockLogger;
        private Mock<ITaskRepository> mockTaskRepository;
        private Mock<IUserRepository> mockUserRepository;
        private Mock<ITaskHistoryRepository> mockTaskHistoryRepository;

        public GetPerformanceReportQueryHandlerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Default);

            this.mockProjectRepository = this.mockRepository.Create<IProjectRepository>();
            this.mockLogger = this.mockRepository.Create<ILogger<GetPerformanceReportQueryHandler>>();
            this.mockTaskRepository = this.mockRepository.Create<ITaskRepository>();
            this.mockUserRepository = this.mockRepository.Create<IUserRepository>();
            this.mockTaskHistoryRepository = this.mockRepository.Create<ITaskHistoryRepository>();
        }

        private GetPerformanceReportQueryHandler CreateGetPerformanceReportQueryHandler()
        {
            return new GetPerformanceReportQueryHandler(
                this.mockProjectRepository.Object,
                this.mockLogger.Object,
                this.mockTaskRepository.Object,
                this.mockUserRepository.Object,
                this.mockTaskHistoryRepository.Object);
        }

        [Fact]
        public async Task Test_Success_Scenario()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            // Arrange
            var getPerformanceReportQueryHandler = this.CreateGetPerformanceReportQueryHandler();
            GetPerformanceReportQuery request = fixture.Create<GetPerformanceReportQuery>();
            CancellationToken cancellationToken = default;
            mockUserRepository.Setup(x => x.GetByIdAsNoTrackingAsync(It.IsAny<int>())).ReturnsAsync(fixture.Create<User>());
            mockUserRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(fixture.Create<User>());
            mockTaskRepository.Setup(x => x.GetAllTasksByUser(It.IsAny<int>())).ReturnsAsync(fixture.CreateMany<Domain.Entities.ProjectTask>(3));

            // Act
            var result = await getPerformanceReportQueryHandler.Handle(
                request,
                cancellationToken);


            result.Should().NotBeNull();
            result.Should().BeOfType<BaseResponse<GetPerformanceReportQueryResponse>>();
            result.Data.Should().NotBeNull();
            result.Data.Should().BeOfType<GetPerformanceReportQueryResponse>();
            result.IsSuccess.Should().BeTrue();
            result.Errors.Should().BeEmpty();

        }
    }
}
