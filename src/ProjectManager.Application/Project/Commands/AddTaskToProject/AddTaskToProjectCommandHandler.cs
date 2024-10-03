using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

namespace ProjectManager.Application.Project.Commands.AddTaskToProject
{
    public class AddTaskToProjectCommandHandler(
        ITaskRepository taskRepository,
        ILogger<AddTaskToProjectCommandHandler> logger)
        : IRequestHandler<AddTaskToProjectCommand, BaseResponse<bool>>
    {
        private readonly ITaskRepository _taskRepository = taskRepository;
        private readonly ILogger<AddTaskToProjectCommandHandler> _logger = logger;

        public async Task<BaseResponse<bool>> Handle(AddTaskToProjectCommand request,
            CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();
            try
            {

                var task = new Domain.Entities.ProjectTask()
                {
                    Priority = request.Priority,
                    ProjectId = request.ProjectId,
                    Title = request.Title,
                    Description = request.Description,
                    TargetDate = request.TargetDate.GetValueOrDefault(),
                    CreatedDate = DateTime.Now
                };
                await _taskRepository.AddAsync(task);
                response.Success(true);
                _logger.LogInformation("Task added to project {projectId}", request.ProjectId);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding task to project {projectId}",
                    request.ProjectId);
                response.AddError($"Error adding task to project {request.ProjectId}");
                return response;
            }
        }
    }
}
