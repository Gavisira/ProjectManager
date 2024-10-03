using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectTask.Queries.GetPerformanceReport
{
    public class GetPerformanceReportQueryHandler : IRequestHandler<GetPerformanceReportQuery, BaseResponse<GetPerformanceReportQueryResponse>>
    {
        public GetPerformanceReportQueryHandler()
        {
        }

        public async Task<BaseResponse<GetPerformanceReportQueryResponse>> Handle(GetPerformanceReportQuery request, CancellationToken cancellationToken)
        {
			throw new NotImplementedException();
        }
    }
}
