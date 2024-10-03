using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectTask.Commands.DeleteTask
{
    public class DeleteTaskCommand : IRequest<BaseResponse<bool>>
    {
        public int TaskId { get; set; }
        public int AssignedUserId { get; set; }

    }
}
