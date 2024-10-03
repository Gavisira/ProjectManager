using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Project.CreateProject
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, BaseResponse<Unit>>
    {
        public CreateProjectCommandHandler()
        {
        }

        public async Task<BaseResponse<Unit>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
			throw new NotImplementedException();
        }
    }
}
