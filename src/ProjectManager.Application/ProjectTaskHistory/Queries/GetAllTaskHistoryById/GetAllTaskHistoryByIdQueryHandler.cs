using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

namespace ProjectManager.Application.ProjectTaskHistory.Queries.GetAllTaskHistoryById;

public class GetAllTaskHistoryByIdQueryHandler(
    ITaskHistoryRepository taskHistoryRepository,
    ILogger<GetAllTaskHistoryByIdQueryHandler> logger)
    : IRequestHandler<GetAllTaskHistoryByIdQuery,
        BaseResponse<GetAllTaskHistoryByIdQueryResponse>>
{
    public async Task<BaseResponse<GetAllTaskHistoryByIdQueryResponse>> Handle(GetAllTaskHistoryByIdQuery request,
        CancellationToken cancellationToken)
    {
        var response = new BaseResponse<GetAllTaskHistoryByIdQueryResponse>();

        try
        {
            var taskHistory = await taskHistoryRepository.GetAllTaskHistoryByTaskId(request.TaskId);
            if (taskHistory == null)
            {
                response.Errors.Add("Task history not found");
                return response;
            }

            var result = new GetAllTaskHistoryByIdQueryResponse
            {
                TaskHistory = taskHistory.Select(x => new TaskHistoryResponse
                {
                    ChangeDate = x.ChangeDate,
                    HistoryDescription = x.HistoryDescription,
                    ProjectTaskId = x.ProjectTaskId,
                    UserId = x.UserId,
                    UserName = x.User.Name
                }).ToList()
            };

            response.Success(result);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while getting task history by id");
            response.Errors.Add("Error while getting task history by id");
            return response;
        }
    }
}