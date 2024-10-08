﻿using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

namespace ProjectManager.Application.Project.Queries.GetAllProjectsFromUser;

public class GetAllProjectsFromUserQueryHandler(
    IProjectRepository projectRepository,
    ILogger<GetAllProjectsFromUserQueryHandler> logger)
    : IRequestHandler<GetAllProjectsFromUserQuery, BaseResponse<GetAllProjectsFromUserQueryResponse>>
{
    private readonly ILogger<GetAllProjectsFromUserQueryHandler> _logger = logger;
    private readonly IProjectRepository _projectRepository = projectRepository;

    public async Task<BaseResponse<GetAllProjectsFromUserQueryResponse>> Handle(GetAllProjectsFromUserQuery request,
        CancellationToken cancellationToken)
    {
        var result = new BaseResponse<GetAllProjectsFromUserQueryResponse>();
        try
        {
            var projects = await _projectRepository.GetAllProjectsFromUserAsync(request.UserId);
            var response = new GetAllProjectsFromUserQueryResponse
            {
                Projects = projects.Select(p => new ProjectResponse
                {
                    Name = p.Name,
                    Description = p.Description,
                    TargetDate = p.TargetDate
                })
            };
            result.Success(response);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting projects from user");
            return result.AddError("Error getting projects from user");
        }
    }
}