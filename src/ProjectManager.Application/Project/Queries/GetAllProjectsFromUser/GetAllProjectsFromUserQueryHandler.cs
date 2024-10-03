using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Project.Queries.GetAllProjectsFromUser
{
    public class GetAllProjectsFromUserQueryHandler : IRequestHandler<GetAllProjectsFromUserQuery, BaseResponse<GetAllProjectsFromUserQueryResponse>>
    {
        public GetAllProjectsFromUserQueryHandler()
        {
        }

        public async Task<BaseResponse<GetAllProjectsFromUserQueryResponse>> Handle(GetAllProjectsFromUserQuery request, CancellationToken cancellationToken)
        {
			throw new NotImplementedException();
        }
    }
}
