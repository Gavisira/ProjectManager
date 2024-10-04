namespace ProjectManager.Application.ProjectTask.Queries.GetAllTasksFromProject;

public class GetAllTasksFromProjectQueryResponse
{
    public List<TaskResponse> Tasks { get; set; }
}

public class TaskResponse
{
    public string Priority { get; set; }
    public string Status { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime TargetDate { get; set; }
    public DateTime EndDate { get; set; }
}