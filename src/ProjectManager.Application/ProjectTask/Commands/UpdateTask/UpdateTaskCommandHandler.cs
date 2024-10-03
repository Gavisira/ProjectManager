using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;
using ProjectManager.Infrastructure.SQLServer.Repositories;
using ProjectManager.Domain.Entities;

namespace ProjectManager.Application.ProjectTask.Commands.UpdateTask
{
    public class UpdateTaskCommandHandler(
        ITaskRepository taskRepository, 
        ILogger<UpdateTaskCommandHandler> logger,
        ITaskHistoryRepository taskHistoryRepository)
        : IRequestHandler<UpdateTaskCommand, BaseResponse<bool>>
    {
        private readonly ITaskRepository _taskRepository = taskRepository;
        private readonly ILogger<UpdateTaskCommandHandler> _logger = logger;
        private readonly ITaskHistoryRepository _taskHistoryRepository = taskHistoryRepository;


        public async Task<BaseResponse<bool>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            //update only not null request parameters
            var response = new BaseResponse<bool>();
            try
            {
                _logger.LogInformation("Updating task {taskId}", request.TaskId);
                var task = await _taskRepository.GetByIdAsync(request.TaskId);
                if (task == null)
                {
                    _logger.LogWarning("Task {taskId} not found", request.TaskId);
                    response.AddError($"Task {request.TaskId} not found");
                    return response;
                }

                if (request.Title != null)
                {
                    task.Title = request.Title;
                }

                if (request.Description != null)
                {
                    task.Description = request.Description;
                }

                if (request.TargetDate != null)
                {
                    task.TargetDate = request.TargetDate.Value;
                }

                var result = await _taskRepository.UpdateAsync(task);
                response.Success(true);
                _logger.LogInformation("Task {taskId} updated", request.TaskId);

                var taskHistory = new Domain.Entities.ProjectTaskHistory()
                {
                    ProjectTaskId = request.TaskId,
                    ChangeDate = DateTime.Now,
                    UserId = request.AssignedUserId,
                    HistoryDescription = $"Task with id {result.Id} updated succeed."
                };

                await _taskHistoryRepository.AddAsync(taskHistory);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating task {taskId}", request.TaskId);
                response.AddError($"Error updating task {request.TaskId}");
                return response;
            }
        }
    }
}
