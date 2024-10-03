using Moq;
using ProjectManager.Application.Project.Queries.GetAllProjectsFromUser;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ProjectManager.UnitTests.Project.Queries.GetAllProjectsFromUser
{
    public class GetAllProjectsFromUserQueryHandlerTests
    {
        private MockRepository mockRepository;



        public GetAllProjectsFromUserQueryHandlerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        private GetAllProjectsFromUserQueryHandler CreateGetAllProjectsFromUserQueryHandler()
        {
            return new GetAllProjectsFromUserQueryHandler();
        }

        [Fact]
        public async Task Handle_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var getAllProjectsFromUserQueryHandler = this.CreateGetAllProjectsFromUserQueryHandler();
            GetAllProjectsFromUserQuery request = null;
            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

            // Act
            var result = await getAllProjectsFromUserQueryHandler.Handle(
                request,
                cancellationToken);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}
