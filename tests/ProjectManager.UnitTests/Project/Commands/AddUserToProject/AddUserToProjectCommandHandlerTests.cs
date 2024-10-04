using Microsoft.Extensions.Logging;
using Moq;
using ProjectManager.Application.Project.Commands.AddUserToProject;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Xunit;

namespace ProjectManager.UnitTests.Project.Commands.AddUserToProject
{
    public class AddUserToProjectCommandHandlerTests
    {
        private MockRepository mockRepository;

        private Mock<IProjectRepository> mockProjectRepository;
        private Mock<ILogger<AddUserToProjectCommandHandler>> mockLogger;

        public AddUserToProjectCommandHandlerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Default);

            this.mockProjectRepository = this.mockRepository.Create<IProjectRepository>();
            this.mockLogger = this.mockRepository.Create<ILogger<AddUserToProjectCommandHandler>>();
        }

        private AddUserToProjectCommandHandler CreateAddUserToProjectCommandHandler()
        {
            return new AddUserToProjectCommandHandler(
                this.mockProjectRepository.Object,
                this.mockLogger.Object);
        }

        [Fact]
        public async Task Handle_StateUnderTest_ExpectedBehavior()
        {
            var projectManagerFixture = new Fixture().Customize(new AutoMoqCustomization());
            projectManagerFixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var addUserToProjectCommandHandler = this.CreateAddUserToProjectCommandHandler();
            var request = projectManagerFixture.Create<AddUserToProjectCommand>();
            CancellationToken cancellationToken = default;

            this.mockProjectRepository.Setup(x => x.AddUserToProject(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(true);

            var result = await addUserToProjectCommandHandler.Handle(
                request,
                cancellationToken);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();

        }

        [Fact]
        public async Task Handle_StateUnderTest_ExpectedBehavior_WithException()
        {
            var projectManagerFixture = new Fixture().Customize(new AutoMoqCustomization());
            projectManagerFixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var addUserToProjectCommandHandler = this.CreateAddUserToProjectCommandHandler();
            var request = projectManagerFixture.Create<AddUserToProjectCommand>();
            CancellationToken cancellationToken = default;
            this.mockProjectRepository.Setup(x => x.AddUserToProject(It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(new Exception());
            var result = await addUserToProjectCommandHandler.Handle(
                request,
                cancellationToken);
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().NotBeNullOrEmpty();
        }
    }
}
