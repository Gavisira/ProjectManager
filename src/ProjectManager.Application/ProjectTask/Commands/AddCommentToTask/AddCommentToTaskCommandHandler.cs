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
        ILogger<AddCommentToTaskCommandHandler> logger)
        : IRequestHandler<AddCommentToTaskCommand, BaseResponse<bool>>
    {
        private readonly ICommentTaskRepository _commentTaskRepository = commentTaskRepository;
        private readonly ILogger<AddCommentToTaskCommandHandler> _logger = logger;


        public async Task<BaseResponse<bool>> Handle(AddCommentToTaskCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var comment = new ProjectTaskComment()
                {
                    ProjectTaskId = request.TaskId,
                    Comment = request.Comment,
                    CreatedDate = DateTime.Now,
                    CreatedUserId = request.UserId
                };
                await _commentTaskRepository.AddAsync(comment);
                response.Success(true);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding comment to task");
                response.AddError("Error adding comment to task");
                return response;
            }
        }
    }
}
