using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManager.Domain.Enums;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

namespace ProjectManager.Application.ProjectTask.Queries.GetPerformanceReport;

public class GetPerformanceReportQueryHandler(
    IProjectRepository projectRepository,
    ILogger<GetPerformanceReportQueryHandler> logger,
    ITaskRepository taskRepository,
    IUserRepository userRepository,
    ITaskHistoryRepository taskHistoryRepository)
    : IRequestHandler<GetPerformanceReportQuery, BaseResponse<GetPerformanceReportQueryResponse>>
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly ITaskHistoryRepository _taskHistoryRepository = taskHistoryRepository;


    public async Task<BaseResponse<GetPerformanceReportQueryResponse>> Handle(GetPerformanceReportQuery request,
        CancellationToken cancellationToken)
    {
        var response = new BaseResponse<GetPerformanceReportQueryResponse>();
        try
        {
            var assigned = await userRepository.GetByIdAsNoTrackingAsync(request.AssignedUserId);

            if (assigned == null)
            {
                response.AddError("Assigned user not found");
                return response;
            }

            if (!assigned.Roles.Contains(EUserRole.Manager))
            {
                response.AddError("User is not a manager");
                return response;
            }

            var user = await userRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                response.Fail(new List<string> { "User not found" });
                return response;
            }

            var tasks = await taskRepository.GetAllTasksByUser(request.UserId);
            tasks = tasks.Where(x => x.CreatedDate.Date >= DateTime.Now.AddDays(-30).Date).ToList();

            if (!tasks.Any())
            {
                logger.LogWarning($"Tasks not found for user {request.UserId} in past 30 days.");
                response.AddError($"Tasks not found for user {request.UserId} in past 30 days.");
                return response;
            }

            var totalTasks = tasks.Count();
            var totalTasksCompleted = tasks.Count(x => x.Status == EProjectTaskStatus.Done);
            var totalTasksPending = tasks.Count(x =>
                x.Status != EProjectTaskStatus.Done && x.Status != EProjectTaskStatus.ToReview);
            var totalTasksInProgress = tasks.Count(x => x.Status == EProjectTaskStatus.InProgress);
            var totalTasksOverdue = tasks.Count(x => x.TargetDate.Date < DateTime.Now);
            var totalTasksCompletedOnTime =
                tasks.Count(x => x.Status == EProjectTaskStatus.Done && x.EndDate <= x.TargetDate.Date);
            var totalTasksCompletedLate =
                tasks.Count(x => x.Status == EProjectTaskStatus.Done && x.EndDate > x.TargetDate.Date);
            var totalTasksCompletedEarly =
                tasks.Count(x => x.Status == EProjectTaskStatus.Done && x.EndDate < x.TargetDate.Date);
            var responseModel = new GetPerformanceReportQueryResponse
            {
                UserId = user.Id,
                UserName = user.Name,
                TotalTasks = totalTasks,
                TotalTasksCompleted = totalTasksCompleted,
                TotalTasksPending = totalTasksPending,
                TotalTasksInProgress = totalTasksInProgress,
                TotalTasksOverdue = totalTasksOverdue,
                TotalTasksCompletedOnTime = totalTasksCompletedOnTime,
                TotalTasksCompletedLate = totalTasksCompletedLate,
                TotalTasksCompletedEarly = totalTasksCompletedEarly,
                ReferenceStartDate = DateTime.Now.AddDays(-30).Date,
                ReferenceEndDate = DateTime.Now.Date
            };
            response.Success(responseModel);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while getting performance report");
            response.Fail(new List<string> { "Error while getting performance report" });
            return response;
        }
    }
}