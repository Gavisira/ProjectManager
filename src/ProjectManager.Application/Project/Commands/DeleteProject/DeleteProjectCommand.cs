using MediatR;

namespace ProjectManager.Application.Project.Commands.DeleteProject;

public class DeleteProjectCommand : IRequest<BaseResponse<bool>>
{
    public int ProjectId { get; set; }
}