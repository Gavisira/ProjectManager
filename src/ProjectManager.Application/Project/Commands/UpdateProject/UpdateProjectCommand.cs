using MediatR;

namespace ProjectManager.Application.Project.Commands.UpdateProject;

public class UpdateProjectCommand : IRequest<BaseResponse<bool>>
{
    public int ProjectId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime? TargetDate { get; set; }
}