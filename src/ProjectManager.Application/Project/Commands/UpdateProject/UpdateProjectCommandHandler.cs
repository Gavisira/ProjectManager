using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

namespace ProjectManager.Application.Project.UpdateProject
{
    public class UpdateProjectCommandHandler(
        IProjectRepository projectRepository,
        ILogger<UpdateProjectCommandHandler> logger)
        : IRequestHandler<UpdateProjectCommand, BaseResponse<bool>>
    {
        private readonly IProjectRepository _projectRepository = projectRepository;
        private readonly ILogger<UpdateProjectCommandHandler> _logger = logger;

        public async Task<BaseResponse<bool>> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();
            try
            {
                _logger.LogInformation("Updating project {projectId}", request.ProjectId);
                var project = await _projectRepository.GetByIdAsync(request.ProjectId);
                if (project == null)
                {
                    _logger.LogWarning("Project {projectId} not found", request.ProjectId);
                    response.AddError($"Project {request.ProjectId} not found");
                    return response;
                }

                if (!string.IsNullOrEmpty(request.Name))
                {
                    project.Name = request.Name;
                }

                if (!string.IsNullOrEmpty(request.Description))
                {
                    project.Description = request.Description;
                }

                if (request.TargetDate != null)
                {
                    project.TargetDate = request.TargetDate.Value;
                }

                await _projectRepository.UpdateAsync(project);
                response.Success(true);
                _logger.LogInformation("Project {projectId} updated", request.ProjectId);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating project {projectId}", request.ProjectId);
                response.AddError($"Error updating project {request.ProjectId}");
                return response;
            }
        }
    }
}
