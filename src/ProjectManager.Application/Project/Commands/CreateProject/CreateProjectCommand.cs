using MediatR;

namespace ProjectManager.Application.Project.Commands.CreateProject;

public class CreateProjectCommand : IRequest<BaseResponse<bool>>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime? TargetDate { get; set; }
}