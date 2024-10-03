using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

namespace ProjectManager.Application.ProjectTask.Commands.DeleteTask;

public class DeleteTaskCommandHandler(
    ITaskRepository taskRepository,
    ILogger<DeleteTaskCommandHandler> logger,
    ITaskHistoryRepository taskHistoryRepository)
    : IRequestHandler<DeleteTaskCommand, BaseResponse<bool>>
{
    private readonly ILogger<DeleteTaskCommandHandler> _logger = logger;
    private readonly ITaskHistoryRepository _taskHistoryRepository = taskHistoryRepository;

    private readonly ITaskRepository _taskRepository = taskRepository;


    public async Task<BaseResponse<bool>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();
        try
        {
            _logger.LogInformation("Deleting task {taskId}", request.TaskId);
            var task = await _taskRepository.GetByIdAsync(request.TaskId);
            if (task == null)
            {
                _logger.LogWarning("Task {taskId} not found", request.TaskId);
                response.AddError($"Task {request.TaskId} not found");
                return response;
            }

            var result = await _taskRepository.DeleteAsync(request.TaskId);
            response.Success(true);
            _logger.LogInformation("Task {taskId} deleted", request.TaskId);


            var taskHistory = new Domain.Entities.ProjectTaskHistory
            {
                ProjectTaskId = request.TaskId,
                ChangeDate = DateTime.Now,
                UserId = request.AssignedUserId,
                HistoryDescription = $"Task with id {request.TaskId} succeed deleted from project {task.ProjectId}."
            };

            await _taskHistoryRepository.AddAsync(taskHistory);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting task {taskId}", request.TaskId);
            response.AddError($"Error deleting task {request.TaskId}");
            return response;
        }
    }
}