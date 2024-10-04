using Microsoft.Extensions.Logging;
using Moq;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManager.Application.Project.Commands.CreateProject;
using Xunit;

namespace ProjectManager.UnitTests.Project.Commands.CreateProject
{

    [TestClass] public class CreateProjectCommandHandlerTests: DefaultFixture
    {
        private MockRepository mockRepository;

        private Mock<IProjectRepository> mockProjectRepository;
        private Mock<ILogger<CreateProjectCommandHandler>> mockLogger;

        public CreateProjectCommandHandlerTests():base()
        {
            this.mockRepository = new MockRepository(MockBehavior.Default);

            this.mockProjectRepository = this.mockRepository.Create<IProjectRepository>();
            this.mockLogger = this.mockRepository.Create<ILogger<CreateProjectCommandHandler>>();
        }

        private CreateProjectCommandHandler CreateCreateProjectCommandHandler()
        {
            return new CreateProjectCommandHandler(
                this.mockProjectRepository.Object,
                this.mockLogger.Object);
        }

        [Fact]
        public async Task Test_Success_Scenario()
        {
            // Arrange
            var createProjectCommandHandler = this.CreateCreateProjectCommandHandler();
            CreateProjectCommand request = ProjectManagerFixture.Create<CreateProjectCommand>();
            CancellationToken cancellationToken = default;

            //setup mocks
            mockProjectRepository.Setup(x => x.AddAsync(It.IsAny<Domain.Entities.Project>())).ReturnsAsync(ProjectManagerFixture.Create<Domain.Entities.Project>());

            // Act
            var result = await createProjectCommandHandler.Handle(
                request,
                cancellationToken);

            // Assert with fluent assertions
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();

            mockRepository.VerifyAll();




        }
    }
}
