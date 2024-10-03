using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Project.UpdateProject
{
    public class UpdateProjectCommand : IRequest<BaseResponse<Unit>>
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime TargetDate { get; set; }
    }
}
