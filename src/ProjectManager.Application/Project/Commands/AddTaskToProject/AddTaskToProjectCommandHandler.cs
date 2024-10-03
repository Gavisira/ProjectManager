using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Project.Commands.AddTaskToProject
{
    public class AddTaskToProjectCommandHandler : IRequestHandler<AddTaskToProjectCommand, BaseResponse<bool>>
    {
        public AddTaskToProjectCommandHandler()
        {
        }

        public async Task<BaseResponse<bool>> Handle(AddTaskToProjectCommand request, CancellationToken cancellationToken)
        {
			throw new NotImplementedException();
        }
    }
}
