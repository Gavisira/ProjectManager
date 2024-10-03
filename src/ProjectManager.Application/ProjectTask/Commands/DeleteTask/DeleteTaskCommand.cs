using MediatR;

namespace ProjectManager.Application.ProjectTask.Commands.DeleteTask;

public class DeleteTaskCommand : IRequest<BaseResponse<bool>>
{
    public int TaskId { get; set; }
    public int AssignedUserId { get; set; }
}