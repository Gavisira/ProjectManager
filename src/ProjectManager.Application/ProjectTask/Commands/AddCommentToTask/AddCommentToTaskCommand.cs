using MediatR;

namespace ProjectManager.Application.ProjectTask.Commands.AddCommentToTask
{


    public class AddCommentToTaskCommand : IRequest<BaseResponse<bool>>
    {
        public int TaskId { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }
    }
}
