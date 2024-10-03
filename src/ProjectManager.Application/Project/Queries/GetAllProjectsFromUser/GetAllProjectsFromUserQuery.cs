using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Project.Queries.GetAllProjectsFromUser
{
    public class GetAllProjectsFromUserQuery : IRequest<BaseResponse<GetAllProjectsFromUserQueryResponse>>
    {
        public int UserId { get; set; }
    }
}
