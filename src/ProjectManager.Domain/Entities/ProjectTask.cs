using ProjectManager.Domain.Enums;

namespace ProjectManager.Domain.Entities;

public class ProjectTask
{
    public EProjectTaskPriority Priority { get; set; }
    public EProjectTaskStatus Status { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime TargetDate { get; set; }
    public DateTime EndDate { get; set; }
    public IEnumerable<string> Comments { get; set; }


    public IEnumerable<ProjectTaskHistory> TaskHistories { get; set; }
    public Project Project { get; set; }
}