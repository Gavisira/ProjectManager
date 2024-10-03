using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectTask.Commands.UpdateTaskComment
{
    public class UpdateTaskCommentCommandHandler : IRequestHandler<UpdateTaskCommentCommand, BaseResponse<bool>>
    {
        public UpdateTaskCommentCommandHandler()
        {
        }

        public async Task<BaseResponse<bool>> Handle(UpdateTaskCommentCommand request, CancellationToken cancellationToken)
        {
			throw new NotImplementedException();
        }
    }
}
