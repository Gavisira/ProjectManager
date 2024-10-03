namespace ProjectManager.Application.ProjectTaskHistory.Queries.GetAllTaskHistoryById;

public class GetAllTaskHistoryByIdQueryResponse
{
    public List<TaskHistoryResponse> TaskHistory { get; set; }
}

public class TaskHistoryResponse
{
    public string HistoryDescription { get; set; }
    public DateTime ChangeDate { get; set; }
    public int ProjectTaskId { get; set; }

    public int UserId { get; set; }
    public string UserName { get; set; }
}