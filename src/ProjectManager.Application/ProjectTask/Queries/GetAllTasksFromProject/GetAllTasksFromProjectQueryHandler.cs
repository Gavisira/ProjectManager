﻿using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

namespace ProjectManager.Application.ProjectTask.Queries.GetAllTasksFromProject;

public class GetAllTasksFromProjectQueryHandler(
    IProjectRepository projectRepository,
    ILogger<GetAllTasksFromProjectQueryHandler> logger)
    : IRequestHandler<GetAllTasksFromProjectQuery, BaseResponse<GetAllTasksFromProjectQueryResponse>>
{
    private readonly ILogger<GetAllTasksFromProjectQueryHandler> _logger = logger;
    private readonly IProjectRepository _projectRepository = projectRepository;

    public async Task<BaseResponse<GetAllTasksFromProjectQueryResponse>> Handle(GetAllTasksFromProjectQuery request,
        CancellationToken cancellationToken)
    {
        var response = new BaseResponse<GetAllTasksFromProjectQueryResponse>();
            var project = await _projectRepository.GetByIdAsNoTrackingAsync(request.ProjectId);
            if (project == null)
            {
                response.Fail(new List<string> { "Project not found" });
                return response;
            }

            var tasks = project.Tasks.Select(t => new TaskResponse
            {
                Priority = t.Priority.ToString(),
                Status = t.Status.ToString(),
                Title = t.Title,
                Description = t.Description,
                CreatedDate = t.CreatedDate,
                TargetDate = t.TargetDate,
                EndDate = t.EndDate
            }).ToList();
            response.Success(new GetAllTasksFromProjectQueryResponse { Tasks = tasks });
            return response; return response;
        }
    }
