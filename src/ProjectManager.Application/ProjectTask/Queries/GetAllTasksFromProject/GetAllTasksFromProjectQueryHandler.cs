using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectTask.Queries.GetAllTasksFromProject
{
    public class GetAllTasksFromProjectQueryHandler : IRequestHandler<GetAllTasksFromProjectQuery, BaseResponse<GetAllTasksFromProjectQueryResponse>>
    {
        public GetAllTasksFromProjectQueryHandler()
        {
        }

        public async Task<BaseResponse<GetAllTasksFromProjectQueryResponse>> Handle(GetAllTasksFromProjectQuery request, CancellationToken cancellationToken)
        {
			throw new NotImplementedException();
        }
    }
}
