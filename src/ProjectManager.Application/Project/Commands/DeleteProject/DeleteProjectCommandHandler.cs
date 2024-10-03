using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Project.DeleteProject
{
    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, BaseResponse<Unit>>
    {
        public DeleteProjectCommandHandler()
        {
        }

        public async Task<BaseResponse<Unit>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
			throw new NotImplementedException();
        }
    }
}
