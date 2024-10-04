using MediatR;

namespace ProjectManager.Application.ProjectTask.Commands.UpdateTaskComment;

public class UpdateTaskCommentCommand : IRequest<BaseResponse<bool>>
{
    public int TaskCommentId { get; set; }
    public string Comment { get; set; }
    public int AssignedUserId { get; set; }
}