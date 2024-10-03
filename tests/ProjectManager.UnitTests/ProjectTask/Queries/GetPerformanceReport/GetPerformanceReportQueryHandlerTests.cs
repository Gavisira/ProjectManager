using Moq;
using ProjectManager.Application.ProjectTask.Queries.GetPerformanceReport;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ProjectManager.UnitTests.ProjectTask.Queries.GetPerformanceReport
{
    public class GetPerformanceReportQueryHandlerTests
    {
        private MockRepository mockRepository;



        public GetPerformanceReportQueryHandlerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        private GetPerformanceReportQueryHandler CreateGetPerformanceReportQueryHandler()
        {
            return new GetPerformanceReportQueryHandler();
        }

        [Fact]
        public async Task Handle_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var getPerformanceReportQueryHandler = this.CreateGetPerformanceReportQueryHandler();
            GetPerformanceReportQuery request = null;
            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

            // Act
            var result = await getPerformanceReportQueryHandler.Handle(
                request,
                cancellationToken);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}
