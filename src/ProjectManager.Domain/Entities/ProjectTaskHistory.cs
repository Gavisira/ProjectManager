namespace ProjectManager.Domain.Entities;

public class ProjectTaskHistory
{
    public string HistoryDescription { get; set; }
    public DateTime ChangeDate { get; set; }
    public User User { get; set; }
}