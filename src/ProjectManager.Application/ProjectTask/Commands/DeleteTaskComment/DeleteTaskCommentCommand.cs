using MediatR;

namespace ProjectManager.Application.ProjectTask.Commands.DeleteTaskComment;

public class DeleteTaskCommentCommand : IRequest<BaseResponse<bool>>
{
    public int TaskCommentId { get; set; }
    public int AssignedUserId { get; set; }
}