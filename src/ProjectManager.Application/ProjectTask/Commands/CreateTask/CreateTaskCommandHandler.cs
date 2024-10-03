using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectTask.Commands.CreateTask
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand,BaseResponse<Unit>>
    {
        public CreateTaskCommandHandler()
        {
        }

        public async Task<BaseResponse<Unit>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
			throw new NotImplementedException();
        }
    }
}
