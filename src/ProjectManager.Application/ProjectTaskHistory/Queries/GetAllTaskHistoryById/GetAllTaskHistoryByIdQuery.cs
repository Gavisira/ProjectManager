using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectTaskHistory.Queries.GetAllTaskHistoryById
{
    public class GetAllTaskHistoryByIdQuery : IRequest<BaseResponse<GetAllTaskHistoryByIdQueryResponse>>
    {
        public int TaskId { get; set; }
    }
}
