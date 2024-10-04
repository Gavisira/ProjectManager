using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

namespace ProjectManager.Application.Project.Commands.CreateProject;

public class CreateProjectCommandHandler(
    IProjectRepository projectRepository,
    ILogger<CreateProjectCommandHandler> logger)
    : IRequestHandler<CreateProjectCommand, BaseResponse<bool>>
{
    private readonly ILogger<CreateProjectCommandHandler> _logger = logger;
    private readonly IProjectRepository _projectRepository = projectRepository;

    public async Task<BaseResponse<bool>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();
        try
        {
            var project = new Domain.Entities.Project
            {
                Name = request.Name,
                Description = request.Description,
                TargetDate = request.TargetDate.GetValueOrDefault()
            };
            await _projectRepository.AddAsync(project);
            response.Success(true);
            _logger.LogInformation("Project added");
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding project");
            response.AddError("Error adding project");
            return response;
        }
    }
}