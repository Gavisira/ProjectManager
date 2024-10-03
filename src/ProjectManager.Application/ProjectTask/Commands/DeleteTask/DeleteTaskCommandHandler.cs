using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectTask.Commands.DeleteTask
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand,BaseResponse<bool>>
    {
        public DeleteTaskCommandHandler()
        {
        }

        public async Task<BaseResponse<bool>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
			throw new NotImplementedException();
        }
    }
}
