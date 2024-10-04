namespace ProjectManager.Application.Project.Queries.GetAllProjectsFromUser;

public class GetAllProjectsFromUserQueryResponse
{
    public IEnumerable<ProjectResponse> Projects { get; set; }
}

public class ProjectResponse
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime TargetDate { get; set; }
}