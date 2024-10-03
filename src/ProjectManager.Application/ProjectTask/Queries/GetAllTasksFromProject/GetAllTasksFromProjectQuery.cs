using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectTask.Queries.GetAllTasksFromProject
{
    public class GetAllTasksFromProjectQuery : IRequest<BaseResponse<GetAllTasksFromProjectQueryResponse>>
    {
        public int ProjectId { get; set; }
    }
}
