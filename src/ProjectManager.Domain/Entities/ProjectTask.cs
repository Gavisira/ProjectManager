using ProjectManager.Domain.Enums;

namespace ProjectManager.Domain.Entities;

public class ProjectTask : BaseEntity
{
    public EProjectTaskPriority Priority { get; set; }
    public EProjectTaskStatus Status { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime TargetDate { get; set; }
    public DateTime EndDate { get; set; }



    public int ProjectId { get; set; }
    public Project Project { get; set; }
    public IEnumerable<ProjectTaskComment> Comments { get; set; }
    public IEnumerable<ProjectTaskHistory> TaskHistories { get; set; }
}