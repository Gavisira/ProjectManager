using MediatR;

namespace ProjectManager.Application.Project.Commands.AddUserToProject;

public class AddUserToProjectCommand : IRequest<BaseResponse<bool>>
{
    public int UserId { get; set; }
    public int ProjectId { get; set; }
}