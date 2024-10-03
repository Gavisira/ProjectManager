using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

namespace ProjectManager.Application.Project.Commands.AddUserToProject
{
    public class AddUserToProjectCommandHandler(
        IProjectRepository projectRepository,
        ILogger<AddUserToProjectCommandHandler> logger
    ) : IRequestHandler<AddUserToProjectCommand,BaseResponse<bool>>
    {
        private readonly IProjectRepository _projectRepository = projectRepository;
        private readonly ILogger<AddUserToProjectCommandHandler> _logger = logger;




        public async Task<BaseResponse<bool>> Handle(AddUserToProjectCommand request, CancellationToken cancellationToken)
        {
            var result = new BaseResponse<bool>();
            try
            {
                var success = await _projectRepository.AddUserToProject(request.UserId, request.ProjectId);
                if (success)
                {
                    result.Success(true);
                    return result;
                }
                else
                {
                    return result.AddError("Error adding user to project");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding user to project");
                return result.AddError("Error adding user to project");
            }
        }
    }
}
