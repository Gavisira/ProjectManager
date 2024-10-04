using MediatR;

namespace ProjectManager.Application.ProjectTask.Queries.GetAllTasksFromProject;

public class GetAllTasksFromProjectQuery : IRequest<BaseResponse<GetAllTasksFromProjectQueryResponse>>
{
    public int ProjectId { get; set; }
}