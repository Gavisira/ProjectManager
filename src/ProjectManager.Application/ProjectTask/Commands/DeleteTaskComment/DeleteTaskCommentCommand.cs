using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectTask.Commands.DeleteTaskComment
{
    public class DeleteTaskCommentCommand : IRequest<BaseResponse<Unit>>
    {
        public int TaskCommentId { get; set; }
    }
}
