using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Project.CreateProject
{
    public class CreateProjectCommand : IRequest<BaseResponse<bool>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? TargetDate { get; set; }

    }
}
