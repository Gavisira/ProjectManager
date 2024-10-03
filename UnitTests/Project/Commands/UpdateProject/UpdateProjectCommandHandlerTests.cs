using Microsoft.Extensions.Logging;
using Moq;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Xunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManager.Application.Project.Commands.UpdateProject;

namespace ProjectManager.UnitTests.Project.Commands.UpdateProject
{
    [TestClass] public class UpdateProjectCommandHandlerTests:DefaultFixture
    {
        private MockRepository mockRepository;

        private Mock<IProjectRepository> mockProjectRepository;
        private Mock<ILogger<UpdateProjectCommandHandler>> mockLogger;

        public UpdateProjectCommandHandlerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Default);

            this.mockProjectRepository = this.mockRepository.Create<IProjectRepository>();
            this.mockLogger = this.mockRepository.Create<ILogger<UpdateProjectCommandHandler>>();
        }

        private UpdateProjectCommandHandler CreateUpdateProjectCommandHandler()
        {
            return new UpdateProjectCommandHandler(
                this.mockProjectRepository.Object,
                this.mockLogger.Object);
        }

        [Fact]
        public async Task Test_Success_Scenario()
        {
            //faça o setup seguindo o exemplo dos outros testes usando ProjectManagerFixture
            // Arrange
            var updateProjectCommandHandler = this.CreateUpdateProjectCommandHandler();
            UpdateProjectCommand request = ProjectManagerFixture.Create<UpdateProjectCommand>();
            CancellationToken cancellationToken = default;

            //setup mocks
            mockProjectRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(ProjectManagerFixture.Create<Domain.Entities.Project>());
            mockProjectRepository.Setup(x => x.UpdateAsync(It.IsAny<Domain.Entities.Project>())).ReturnsAsync(ProjectManagerFixture.Create<Domain.Entities.Project>());

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
}
