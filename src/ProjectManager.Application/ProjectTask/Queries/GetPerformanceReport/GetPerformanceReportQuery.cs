using MediatR;

namespace ProjectManager.Application.ProjectTask.Queries.GetPerformanceReport;

public class GetPerformanceReportQuery : IRequest<BaseResponse<GetPerformanceReportQueryResponse>>
{
    public int UserId { get; set; }
    public int AssignedUserId { get; set; }
}