using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectTask.Commands.DeleteTaskComment
{
    public class DeleteTaskCommentCommandHandler : IRequestHandler<DeleteTaskCommentCommand,BaseResponse<bool>>
    {
        public DeleteTaskCommentCommandHandler()
        {
        }

        public async Task<BaseResponse<bool>> Handle(DeleteTaskCommentCommand request, CancellationToken cancellationToken)
        {
			throw new NotImplementedException();
        }
    }
}
