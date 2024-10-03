﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Domain.Enums;

namespace ProjectManager.Application.Project.Commands.AddTaskToProject
{
    public class AddTaskToProjectCommand : IRequest<BaseResponse<bool>>
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public EProjectTaskPriority Priority { get; set; }
        public DateTime? TargetDate { get; set; }
        public int? AssignedUserId { get; set; }
    }
}
