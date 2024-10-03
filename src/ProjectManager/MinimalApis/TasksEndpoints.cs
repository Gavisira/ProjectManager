using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Application.Project.Commands.UpdateProject;
using ProjectManager.Application.ProjectTask.Commands.AddCommentToTask;
using ProjectManager.Application.ProjectTask.Commands.CreateTask;
using ProjectManager.Application.ProjectTask.Commands.DeleteTask;
using ProjectManager.Application.ProjectTask.Commands.DeleteTaskComment;
using ProjectManager.Application.ProjectTask.Queries.GetAllTasksFromProject;
using ProjectManager.Application.ProjectTask.Queries.GetPerformanceReport;
using ProjectManager.Application.ProjectTaskHistory.Queries.GetAllTaskHistoryById;

namespace ProjectManager.MinimalApis;

public static class TasksEndpoints
{
    public static WebApplication SetupTasksEndpoints(this WebApplication app)
    {
        app.MapPost("/tasks/{taskId}/comments",
            async ([FromServices] IMediator mediator, AddCommentToTaskCommand command) =>
            {
                var response = await mediator.Send(command);
                return Results.Ok(response);
            }).WithOpenApi().WithTags("Task Comments");

        app.MapPost("/tasks", async ([FromServices] IMediator mediator, CreateTaskCommand command) =>
        {
            var response = await mediator.Send(command);
            return Results.Ok(response);
        }).WithOpenApi().WithTags("Tasks");

        app.MapDelete("/tasks/{taskId}", async ([FromServices] IMediator mediator, int taskId) =>
        {
            var response = await mediator.Send(new DeleteTaskCommand { TaskId = taskId });
            return Results.Ok(response);
        }).WithOpenApi().WithTags("Tasks");

        app.MapPut("/tasks/{taskId}",
            async ([FromServices] IMediator mediator, int taskId, UpdateProjectCommand command) =>
            {
                command.ProjectId = taskId;
                var response = await mediator.Send(command);
                return Results.Ok(response);
            }).WithOpenApi().WithTags("Tasks");

        app.MapDelete("/tasks/comments/{commentId}", async ([FromServices] IMediator mediator, int commentId) =>
        {
            var response = await mediator.Send(new DeleteTaskCommentCommand { TaskCommentId = commentId });
            return Results.Ok(response);
        }).WithOpenApi().WithTags("Task Comments");

        app.MapPut("/tasks/comments/{commentId}",
            async ([FromServices] IMediator mediator, int commentId, UpdateProjectCommand command) =>
            {
                command.ProjectId = commentId;
                var response = await mediator.Send(command);
                return Results.Ok(response);
            }).WithOpenApi().WithTags("Task Comments");

        app.MapGet("/tasks/{projectId}", async ([FromServices] IMediator mediator, int projectId) =>
        {
            var response = await mediator.Send(new GetAllTasksFromProjectQuery { ProjectId = projectId });
            return Results.Ok(response);
        }).WithOpenApi().WithTags("Tasks");

        app.MapGet("/tasks/performance-report/{userId}/{assignedUserId}",
            async ([FromServices] IMediator mediator, int userId, int assignedUserId) =>
            {
                var response = await mediator.Send(new GetPerformanceReportQuery
                    { UserId = userId, AssignedUserId = assignedUserId });
                return Results.Ok(response);
            }).WithOpenApi().WithTags("Performance report");

        app.MapGet("/tasks/history/{taskId}", async ([FromServices] IMediator mediator, int taskId) =>
        {
            var response = await mediator.Send(new GetAllTaskHistoryByIdQuery { TaskId = taskId });
            return Results.Ok(response);
        }).WithOpenApi().WithTags("Task History");


        return app;
    }
}