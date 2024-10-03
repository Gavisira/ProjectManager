using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectTask.Commands.AddCommentToTask
{
    public class AddCommentToTaskCommand : IRequest<BaseResponse<Unit>>
    {
        public int TaskId { get; set; }
        public string Comment { get; set; }
    }
}
