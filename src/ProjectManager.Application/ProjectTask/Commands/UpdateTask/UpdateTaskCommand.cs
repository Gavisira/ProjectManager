using MediatR;
using ProjectManager.Domain.Enums;

namespace ProjectManager.Application.ProjectTask.Commands.UpdateTask;

public class UpdateTaskCommand : IRequest<BaseResponse<bool>>
{
    public int TaskId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? TargetDate { get; set; }
    public int? ProjectId { get; set; }
    public EProjectTaskStatus? Status { get; set; }
    public int AssignedUserId { get; set; }
}