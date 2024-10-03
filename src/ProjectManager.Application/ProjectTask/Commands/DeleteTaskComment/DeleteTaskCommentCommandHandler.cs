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

namespace ProjectManager.Application.ProjectTask.Commands.DeleteTaskComment
{
    public class DeleteTaskCommentCommandHandler(
        ICommentTaskRepository commentTaskRepository,
        ILogger<DeleteTaskCommentCommandHandler> logger,
        ITaskHistoryRepository taskHistoryRepository)
        : IRequestHandler<DeleteTaskCommentCommand, BaseResponse<bool>>
    {
        private readonly ICommentTaskRepository _commentTaskRepository = commentTaskRepository;
        private readonly ILogger<DeleteTaskCommentCommandHandler> _logger = logger;
        private readonly ITaskHistoryRepository _taskHistoryRepository = taskHistoryRepository;


        public async Task<BaseResponse<bool>> Handle(DeleteTaskCommentCommand request,
            CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();
            try
            {
                _logger.LogInformation("Deleting comment {commentId}", request.TaskCommentId);
                var comment = await _commentTaskRepository.GetByIdAsync(request.TaskCommentId);
                if (comment == null)
                {
                    _logger.LogWarning("Comment {commentId} not found", request.TaskCommentId);
                    response.AddError($"Comment {request.TaskCommentId} not found");
                    return response;
                }

                var result =await _commentTaskRepository.DeleteAsync(request.TaskCommentId);
                response.Success(true);
                _logger.LogInformation("Comment {commentId} deleted", request.TaskCommentId);


                var taskHistory = new ProjectTaskHistory()
                {
                    ProjectTaskId = comment.ProjectTaskId,
                    ChangeDate = DateTime.Now,
                    UserId = request.AssignedUserId,
                    HistoryDescription = $"Comment {comment.Comment} with id {request.TaskCommentId} deleted from task {comment.ProjectTaskId}."
                };
                await _taskHistoryRepository.AddAsync(taskHistory);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting comment {commentId}", request.TaskCommentId);
                response.AddError($"Error deleting comment {request.TaskCommentId}");
                return response;
            }
        }
    }
}
