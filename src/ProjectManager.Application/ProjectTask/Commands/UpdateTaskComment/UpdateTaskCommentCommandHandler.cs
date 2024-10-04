using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

namespace ProjectManager.Application.ProjectTask.Commands.UpdateTaskComment;

public class UpdateTaskCommentCommandHandler(
    ICommentTaskRepository commentTaskRepository,
    ILogger<UpdateTaskCommentCommandHandler> logger,
    ITaskHistoryRepository taskHistoryRepository)
    : IRequestHandler<UpdateTaskCommentCommand, BaseResponse<bool>>
{
    private readonly ICommentTaskRepository _commentTaskRepository = commentTaskRepository;
    private readonly ILogger<UpdateTaskCommentCommandHandler> _logger = logger;
    private readonly ITaskHistoryRepository _taskHistoryRepository = taskHistoryRepository;


    public async Task<BaseResponse<bool>> Handle(UpdateTaskCommentCommand request,
        CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();
        try
        {
            _logger.LogInformation("Updating comment {commentId}", request.TaskCommentId);
            var comment = await _commentTaskRepository.GetByIdAsync(request.TaskCommentId);
            if (comment == null)
            {
                _logger.LogWarning("Comment {commentId} not found", request.TaskCommentId);
                response.AddError($"Comment {request.TaskCommentId} not found");
                return response;
            }

            comment.Comment = request.Comment;
            var result = await _commentTaskRepository.UpdateAsync(comment);
            response.Success(true);
            _logger.LogInformation("Comment {commentId} updated", request.TaskCommentId);


            var taskHistory = new Domain.Entities.ProjectTaskHistory
            {
                ProjectTaskId = comment.ProjectTaskId,
                ChangeDate = DateTime.Now,
                UserId = request.AssignedUserId,
                HistoryDescription =
                    $"Comment {result.Comment} with id {result.Id} added to task {comment.ProjectTaskId}."
            };

            await _taskHistoryRepository.AddAsync(taskHistory);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating comment {commentId}", request.TaskCommentId);
            response.AddError($"Error updating comment {request.TaskCommentId}");
            return response;
        }
    }
}