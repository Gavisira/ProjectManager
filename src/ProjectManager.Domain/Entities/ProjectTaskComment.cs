namespace ProjectManager.Domain.Entities;

public class ProjectTaskComment : BaseEntity
{
    public string Comment { get; set; }
    public DateTime CreatedDate { get; set; }


    public int ProjectTaskId { get; set; }
    public ProjectTask ProjectTask { get; set; }

    public int CreatedUserId { get; set; }
    public User CreatedUser { get; set; }
}