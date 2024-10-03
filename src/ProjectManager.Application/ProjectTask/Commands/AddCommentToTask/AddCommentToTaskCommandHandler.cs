using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectTask.Commands.AddCommentToTask
{
    public class AddCommentToTaskCommandHandler : IRequestHandler<AddCommentToTaskCommand,BaseResponse<Unit>>
    {


        public async Task<BaseResponse<Unit>> Handle(AddCommentToTaskCommand request, CancellationToken cancellationToken)
        {
			throw new NotImplementedException();
        }
    }
}
