using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

namespace ProjectManager.Application.Project.Commands.DeleteProject;

public class DeleteProjectCommandHandler(
    IProjectRepository projectRepository,
    ILogger<DeleteProjectCommandHandler> logger) : IRequestHandler<DeleteProjectCommand, BaseResponse<bool>>
{
    private readonly ILogger<DeleteProjectCommandHandler> _logger = logger;

    private readonly IProjectRepository _projectRepository = projectRepository;


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