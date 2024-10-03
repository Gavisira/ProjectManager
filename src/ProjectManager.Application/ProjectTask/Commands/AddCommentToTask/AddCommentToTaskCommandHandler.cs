using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ProjectManager.Domain.Entities;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

namespace ProjectManager.Application.ProjectTask.Commands.AddCommentToTask
{
    public class AddCommentToTaskCommandHandler(
        ICommentTaskRepository commentTaskRepository,
        ILogger<AddCommentToTaskCommandHandler> logger,
        ITaskHistoryRepository taskHistoryRepository)
        : IRequestHandler<AddCommentToTaskCommand, BaseResponse<bool>>
    {
        private readonly ICommentTaskRepository _commentTaskRepository = commentTaskRepository;
        private readonly ILogger<AddCommentToTaskCommandHandler> _logger = logger;
        private readonly ITaskHistoryRepository _taskHistoryRepository = taskHistoryRepository;



        public async Task<BaseResponse<bool>> Handle(AddCommentToTaskCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();
            try
            {
                _logger.LogInformation("Adding comment to task {taskId}", request.TaskId);
                var comment = new ProjectTaskComment()
                {
                    ProjectTaskId = request.TaskId,
                    Comment = request.Comment,
                    CreatedDate = DateTime.Now,
                    CreatedUserId = request.AssignedUserId
                };
                var result = await _commentTaskRepository.AddAsync(comment);
                response.Success(true);
                _logger.LogInformation("Comment added to task {taskId}", request.TaskId);
                var taskHistory = new ProjectTaskHistory()
                {
                    ProjectTaskId = request.TaskId,
                    ChangeDate = DateTime.Now,
                    UserId = request.AssignedUserId,
                    HistoryDescription = $"Comment {result.Comment} with id {result.Id} added to task {request.TaskId}."
                };

                await _taskHistoryRepository.AddAsync(taskHistory);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding comment to task {taskId}", request.TaskId);
                response.AddError($"Error adding comment to task {request.TaskId}");
                return response;
            }
        }
    }
}
