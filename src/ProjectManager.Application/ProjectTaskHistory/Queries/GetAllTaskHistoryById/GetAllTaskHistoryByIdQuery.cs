using MediatR;

namespace ProjectManager.Application.ProjectTaskHistory.Queries.GetAllTaskHistoryById;

public class GetAllTaskHistoryByIdQuery : IRequest<BaseResponse<GetAllTaskHistoryByIdQueryResponse>>
{
    public int TaskId { get; set; }
}