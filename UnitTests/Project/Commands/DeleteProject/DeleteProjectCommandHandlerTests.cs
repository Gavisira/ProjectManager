using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManager.Application.Project.Commands.DeleteProject;
using Xunit;

namespace ProjectManager.UnitTests.Project.Commands.DeleteProject
{
    [TestClass] public class DeleteProjectCommandHandlerTests:DefaultFixture
    {
        private MockRepository mockRepository;

        private Mock<IProjectRepository> mockProjectRepository;
        private Mock<ILogger<DeleteProjectCommandHandler>> mockLogger;

        public DeleteProjectCommandHandlerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Default);

            this.mockProjectRepository = this.mockRepository.Create<IProjectRepository>();
            this.mockLogger = this.mockRepository.Create<ILogger<DeleteProjectCommandHandler>>();
        }

        private DeleteProjectCommandHandler CreateDeleteProjectCommandHandler()
        {
            return new DeleteProjectCommandHandler(
                this.mockProjectRepository.Object,
                this.mockLogger.Object);
        }

        [Fact]
        public async Task Test_Success_Scenario()
        {
            //create tests using ProjectManagerFixture
            // Arrange
            var deleteProjectCommandHandler = this.CreateDeleteProjectCommandHandler();
            DeleteProjectCommand request = ProjectManagerFixture.Create<DeleteProjectCommand>();
            CancellationToken cancellationToken = default;

            //setup mocks
            mockProjectRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(ProjectManagerFixture.Create<Domain.Entities.Project>());
            mockProjectRepository.Setup(x => x.DeleteAsync(It.IsAny<int>())).ReturnsAsync(true);

            // Act
            var result = await deleteProjectCommandHandler.Handle(
                request,
                cancellationToken);

            // Assert with fluent assertions
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();




        }
    }
}
