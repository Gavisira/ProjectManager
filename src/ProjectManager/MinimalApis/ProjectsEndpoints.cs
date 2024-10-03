using MediatR;
using ProjectManager.Application.Project.CreateProject;
using ProjectManager.Application.Project.DeleteProject;
using ProjectManager.Application.Project.Queries.GetAllProjectsFromUser;
using ProjectManager.Application.Project.UpdateProject;

namespace ProjectManager.MinimalApis;

public static class ProjectsEndpoints
{
    public static WebApplication SetupProjectsEndpoints(this WebApplication app)
    {
        app.MapGet("/projects/{userId}", async (IMediator mediator, int userId) =>
        {
            var response = await mediator.Send(new GetAllProjectsFromUserQuery { UserId = userId });
            return Results.Ok(response);
        }).WithOpenApi();


        //map create project endpoint
        app.MapPost("/projects", async (IMediator mediator, CreateProjectCommand command) =>
        {
            var response = await mediator.Send(command);
            return Results.Ok(response);
        }).WithOpenApi();

        app.MapDelete("/projects/{projectId}", async (IMediator mediator, int projectId) =>
        {
            var response = await mediator.Send(new DeleteProjectCommand { ProjectId = projectId });
            return Results.Ok(response);
        }).WithOpenApi();

        app.MapPut("/projects/{projectId}", async (IMediator mediator, int projectId, UpdateProjectCommand command) =>
        {
            command.ProjectId = projectId;
            var response = await mediator.Send(command);
            return Results.Ok(response);
        }).WithOpenApi();


        return app;
    }
    
}