using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

namespace ProjectManager.Application.ProjectTask.Commands.CreateTask;

public class CreateTaskCommandHandler(
    ITaskRepository taskRepository,
    ILogger<CreateTaskCommandHandler> logger,
    ITaskHistoryRepository taskHistoryRepository)
    : IRequestHandler<CreateTaskCommand, BaseResponse<bool>>
{
    private readonly ILogger<CreateTaskCommandHandler> _logger = logger;
    private readonly ITaskHistoryRepository _taskHistoryRepository = taskHistoryRepository;
    private readonly ITaskRepository _taskRepository = taskRepository;

    public async Task<BaseResponse<bool>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();
        try
        {
            _logger.LogInformation("Creating task {title} in project {project}", request.Title, request.ProjectId);
            var task = new Domain.Entities.ProjectTask
            {
                ProjectId = request.ProjectId,
                Title = request.Title,
                Description = request.Description,
                TargetDate = request.TargetDate,
                Priority = request.Priority
            };
            var result = await _taskRepository.AddAsync(task);
            response.Success(true);
            _logger.LogInformation("Task {title} created in project {project}", request.Title, request.ProjectId);


            var taskHistory = new Domain.Entities.ProjectTaskHistory
            {
                ProjectTaskId = result.Id,
                ChangeDate = DateTime.Now,
                UserId = request.AssignedUserId,
                HistoryDescription = $"Task with id {result.Id} added to project {request.ProjectId}."
            };

            await _taskHistoryRepository.AddAsync(taskHistory);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating task {title} in project {project}", request.Title, request.ProjectId);
            response.AddError($"Error creating task {request.Title} in project {request.ProjectId}");
            return response;
        }
    }
}