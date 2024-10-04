using MediatR;

namespace ProjectManager.Application.Project.Queries.GetAllProjectsFromUser;

public class GetAllProjectsFromUserQuery : IRequest<BaseResponse<GetAllProjectsFromUserQueryResponse>>
{
    public int UserId { get; set; }
}