using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Project.Commands.AddUserToProject
{
    public class AddUserToProjectCommand : IRequest<BaseResponse<bool>>
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
    }
}
