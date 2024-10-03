using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Project.DeleteProject
{
    public class DeleteProjectCommand : IRequest<BaseResponse<Unit>>
    {
        public int ProjectId { get; set; }
    }
}
