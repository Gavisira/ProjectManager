using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectTask.Queries.GetPerformanceReport
{
    public class GetPerformanceReportQuery : IRequest<BaseResponse<GetPerformanceReportQueryResponse>>
    {
        public int UserId { get; set; }
    }
}
