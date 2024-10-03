using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

namespace ProjectManager.Application.ProjectTask.Queries.GetAllTasksFromProject
{
    public class GetAllTasksFromProjectQueryHandler(
        IProjectRepository projectRepository,
        ILogger<GetAllTasksFromProjectQueryHandler> logger)
        : IRequestHandler<GetAllTasksFromProjectQuery, BaseResponse<GetAllTasksFromProjectQueryResponse>>
    {
        private readonly IProjectRepository _projectRepository = projectRepository;
        private readonly ILogger<GetAllTasksFromProjectQueryHandler> _logger = logger;

        public async Task<BaseResponse<GetAllTasksFromProjectQueryResponse>> Handle(GetAllTasksFromProjectQuery request,
            CancellationToken cancellationToken)
        {
            var response = new BaseResponse<GetAllTasksFromProjectQueryResponse>();
            try
            {
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
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting tasks from project");
                response.Fail(new List<string> { "Error while getting tasks from project" });
                return response;
            }

        }
    }
}
