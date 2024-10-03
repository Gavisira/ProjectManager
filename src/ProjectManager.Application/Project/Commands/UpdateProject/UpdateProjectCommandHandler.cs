using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Project.UpdateProject
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, BaseResponse<Unit>>
    {
        public UpdateProjectCommandHandler()
        {
        }

        public async Task<BaseResponse<Unit>> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
			throw new NotImplementedException();
        }
    }
}
