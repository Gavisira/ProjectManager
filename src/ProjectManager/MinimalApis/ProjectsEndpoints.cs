using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Application.Project.Commands.CreateProject;
using ProjectManager.Application.Project.Commands.DeleteProject;
using ProjectManager.Application.Project.Commands.UpdateProject;
using ProjectManager.Application.Project.Queries.GetAllProjectsFromUser;
using System.Diagnostics.CodeAnalysis;

namespace ProjectManager.MinimalApis;
public static class ProjectsEndpoints
{
    public static WebApplication SetupProjectsEndpoints(this WebApplication app)
    {
        app.MapGet("/projects/{userId}", async ([FromServices] IMediator mediator, int userId) =>
        {
            var response = await mediator.Send(new GetAllProjectsFromUserQuery { UserId = userId });
            return Results.Ok(response);
        }).WithOpenApi().WithTags("Projects");


        //map create project endpoint
        app.MapPost("/projects", async ([FromServices] IMediator mediator, CreateProjectCommand command) =>
        {
            var response = await mediator.Send(command);
            return Results.Ok(response);
        }).WithOpenApi().WithTags("Projects");

        app.MapDelete("/projects/{projectId}", async ([FromServices] IMediator mediator, int projectId) =>
        {
            var response = await mediator.Send(new DeleteProjectCommand { ProjectId = projectId });
            return Results.Ok(response);
        }).WithOpenApi().WithTags("Projects");

        app.MapPut("/projects/{projectId}",
            async ([FromServices] IMediator mediator, int projectId, UpdateProjectCommand command) =>
            {
                command.ProjectId = projectId;
                var response = await mediator.Send(command);
                return Results.Ok(response);
            }).WithOpenApi().WithTags("Projects");


        return app;
    }
}