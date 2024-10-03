using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ProjectManager.Application.Project.CreateProject;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;
using ProjectManager.Infrastructure.SQLServer.Repositories;

namespace ProjectManager.Application.Project.DeleteProject
{
    public class DeleteProjectCommandHandler(IProjectRepository projectRepository, ILogger<DeleteProjectCommandHandler> logger) : IRequestHandler<DeleteProjectCommand, BaseResponse<bool>>
    {

        private readonly IProjectRepository _projectRepository = projectRepository;
        private readonly ILogger<DeleteProjectCommandHandler> _logger = logger;



        public async Task<BaseResponse<bool>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();
            try
            {
                _logger.LogInformation("Deleting project {projectId}", request.ProjectId);
                var project = await _projectRepository.GetByIdAsync(request.ProjectId);
                if (project == null)
                {
                    _logger.LogWarning("Project {projectId} not found", request.ProjectId);
                    response.AddError($"Project {request.ProjectId} not found");
                    return response;
                }

                if (project.Tasks.Any())
                {
                    _logger.LogWarning("Project {projectId} has tasks", request.ProjectId);
                    response.AddError($"Project {request.ProjectId} has tasks");
                    return response;
                }

                await _projectRepository.DeleteAsync(request.ProjectId);
                response.Success(true);
                _logger.LogInformation("Project {projectId} deleted", request.ProjectId);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting project {projectId}", request.ProjectId);
                response.AddError($"Error deleting project {request.ProjectId}");
                return response;
            }
        }
    }
}
