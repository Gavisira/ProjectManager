using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Application.Project.Commands.AddUserToProject;
using ProjectManager.Domain.Entities;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

namespace ProjectManager.MinimalApis;

public static class UsersEndpoints
{
    public static WebApplication SetupUserEndpoints(this WebApplication app)
    {
        app.MapPost("/users", async ([FromServices] IUserRepository repository, User command) =>
        {
            var response = await repository.AddAsync(command);
            return Results.Ok(response);
        }).WithOpenApi().WithTags("Users");

        app.MapPost("/users/{userId}/projects/{projectId}",
            async ([FromServices] IMediator mediator, int userId, int projectId) =>
            {
                var response = await mediator.Send(new AddUserToProjectCommand
                    { UserId = userId, ProjectId = projectId });
                return Results.Ok(response);
            }).WithOpenApi().WithTags("Users");


        return app;
    }
}