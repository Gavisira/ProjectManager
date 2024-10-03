﻿using MediatR;
using ProjectManager.Application.Project.CreateProject;
using ProjectManager.Application.Project.UpdateProject;
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
        //map add coment to task endpoint
        app.MapPost("/tasks/{taskId}/comments", async (IMediator mediator, AddCommentToTaskCommand command) =>
        {
            var response = await mediator.Send(command);
            return Results.Ok(response);
        }).WithOpenApi();

        //map createTask endpoint
        app.MapPost("/tasks", async (IMediator mediator, CreateTaskCommand command) =>
        {
            var response = await mediator.Send(command);
            return Results.Ok(response);
        }).WithOpenApi();

        //map delete task endpoint
        app.MapDelete("/tasks/{taskId}", async (IMediator mediator, int taskId) =>
        {
            var response = await mediator.Send(new DeleteTaskCommand { TaskId = taskId });
            return Results.Ok(response);
        }).WithOpenApi();

        //map update task endpoint
        app.MapPut("/tasks/{taskId}", async (IMediator mediator, int taskId, UpdateProjectCommand command) =>
        {
            command.ProjectId = taskId;
            var response = await mediator.Send(command);
            return Results.Ok(response);
        }).WithOpenApi();

        app.MapDelete("/tasks/comments/{commentId}", async (IMediator mediator, int commentId) =>
        {
            var response = await mediator.Send(new DeleteTaskCommentCommand { TaskCommentId = commentId });
            return Results.Ok(response);
        }).WithOpenApi();

        app.MapPut("/tasks/comments/{commentId}", async (IMediator mediator, int commentId, UpdateProjectCommand command) =>
        {
            command.ProjectId = commentId;
            var response = await mediator.Send(command);
            return Results.Ok(response);
        }).WithOpenApi();

        app.MapGet("/tasks/{projectId}", async (IMediator mediator, int projectId) =>
        {
            var response = await mediator.Send(new GetAllTasksFromProjectQuery { ProjectId = projectId });
            return Results.Ok(response);
        }).WithOpenApi();

        app.MapGet("/tasks/performance-report/{userId}/{assignedUserId}", async (IMediator mediator, int userId, int assignedUserId) =>
        {
            var response = await mediator.Send(new GetPerformanceReportQuery { UserId = userId, AssignedUserId = assignedUserId });
            return Results.Ok(response);
        }).WithOpenApi();

        app.MapGet("/tasks/history/{taskId}", async (IMediator mediator, int taskId) =>
        {
            var response = await mediator.Send(new GetAllTaskHistoryByIdQuery { TaskId = taskId });
            return Results.Ok(response);
        }).WithOpenApi();


        return app;
    }
}