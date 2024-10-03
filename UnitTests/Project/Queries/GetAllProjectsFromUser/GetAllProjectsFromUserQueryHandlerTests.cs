using Microsoft.Extensions.Logging;
using Moq;
using ProjectManager.Application.Project.Queries.GetAllProjectsFromUser;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;

namespace ProjectManager.UnitTests.Project.Queries.GetAllProjectsFromUser
{
    [TestClass] public class GetAllProjectsFromUserQueryHandlerTests:DefaultFixture
    {
        private MockRepository mockRepository;

        private Mock<IProjectRepository> mockProjectRepository;
        private Mock<ILogger<GetAllProjectsFromUserQueryHandler>> mockLogger;

        public GetAllProjectsFromUserQueryHandlerTests():base()
        {
            this.mockRepository = new MockRepository(MockBehavior.Default);

            this.mockProjectRepository = this.mockRepository.Create<IProjectRepository>();
            this.mockLogger = this.mockRepository.Create<ILogger<GetAllProjectsFromUserQueryHandler>>();
        }

        private GetAllProjectsFromUserQueryHandler CreateGetAllProjectsFromUserQueryHandler()
        {
            return new GetAllProjectsFromUserQueryHandler(
                this.mockProjectRepository.Object,
                this.mockLogger.Object);
        }

        [Fact]
        public async Task Test_Success_Scenario()
        {
            //faça o setup seguindo o exemplo dos outros testes usando ProjectManagerFixture no request

            // Arrange
            var getAllProjectsFromUserQueryHandler = this.CreateGetAllProjectsFromUserQueryHandler();

            GetAllProjectsFromUserQuery request = ProjectManagerFixture.Create<GetAllProjectsFromUserQuery>();
            CancellationToken cancellationToken = default;

            //setup mocks
            mockProjectRepository.Setup(x => x.GetAllProjectsFromUserAsync(It.IsAny<int>())).ReturnsAsync(ProjectManagerFixture.CreateMany<Domain.Entities.Project>());

            // Act
            var result = await getAllProjectsFromUserQueryHandler.Handle(
                request,
                cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Data.Projects.Count().Should().Be(3);
            result.Errors.Should().BeNullOrEmpty();






        }
    }
}
