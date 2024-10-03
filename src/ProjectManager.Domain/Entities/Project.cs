namespace ProjectManager.Domain.Entities;

public class Project
{
    public string Name { get; set; }



    public IEnumerable<ProjectTask> Tasks { get; set; }
    public IEnumerable<User> Users { get; set; }
}