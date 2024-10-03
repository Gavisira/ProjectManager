using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ProjectManager.Application.ProjectTask.Commands.CreateTask;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;
using ProjectManager.Infrastructure.SQLServer.Repositories;
using ProjectManager.Domain.Entities;

namespace ProjectManager.Application.ProjectTask.Commands.DeleteTask
{
    public class DeleteTaskCommandHandler(
        ITaskRepository taskRepository, 
        ILogger<DeleteTaskCommandHandler> logger,
        ITaskHistoryRepository taskHistoryRepository) 
        : IRequestHandler<DeleteTaskCommand,BaseResponse<bool>>
    {

        private readonly ITaskRepository _taskRepository = taskRepository;
        private readonly ILogger<DeleteTaskCommandHandler> _logger = logger;
        private readonly ITaskHistoryRepository _taskHistoryRepository = taskHistoryRepository;


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



                var taskHistory = new ProjectTaskHistory()
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
}
