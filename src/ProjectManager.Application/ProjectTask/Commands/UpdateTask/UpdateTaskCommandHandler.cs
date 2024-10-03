using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectTask.Commands.UpdateTask
{
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand,BaseResponse<Unit>>
    {
        public UpdateTaskCommandHandler()
        {
        }

        public async Task<BaseResponse<Unit>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
			throw new NotImplementedException();
        }
    }
}
