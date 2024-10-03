using MediatR;
using ProjectManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectTask.Commands.UpdateTask
{
    public class UpdateTaskCommand : IRequest<BaseResponse<bool>>
    {
        public int? TaskId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? TargetDate { get; set; }
        public int? ProjectId { get; set; }
        public EProjectTaskStatus? Status { get; set; }

    }
}
