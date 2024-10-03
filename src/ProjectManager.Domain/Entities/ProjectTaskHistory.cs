namespace ProjectManager.Domain.Entities;

public class ProjectTaskHistory: BaseEntity
{
    public string HistoryDescription { get; set; }
    public DateTime ChangeDate { get; set; }
    public int ProjectTaskId { get; set; }
    public ProjectTask ProjectTask { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
}