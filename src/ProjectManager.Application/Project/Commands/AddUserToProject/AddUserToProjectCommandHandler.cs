using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

namespace ProjectManager.Application.Project.Commands.AddUserToProject;

public class AddUserToProjectCommandHandler(
    IProjectRepository projectRepository,
    ILogger<AddUserToProjectCommandHandler> logger
) : IRequestHandler<AddUserToProjectCommand, BaseResponse<bool>>
{
    private readonly ILogger<AddUserToProjectCommandHandler> _logger = logger;
    private readonly IProjectRepository _projectRepository = projectRepository;


    public async Task<BaseResponse<bool>> Handle(AddUserToProjectCommand request, CancellationToken cancellationToken)
    {
        var result = new BaseResponse<bool>();
        try
        {
            var success = await _projectRepository.AddUserToProject(request.UserId, request.ProjectId);
            if (success)
            {
                result.Success(true);
                return result;
            }

            return result.AddError("Error adding user to project");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding user to project");
            return result.AddError("Error adding user to project");
        }
    }
}