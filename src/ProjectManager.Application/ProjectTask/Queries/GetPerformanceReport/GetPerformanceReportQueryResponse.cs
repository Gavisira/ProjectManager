namespace ProjectManager.Application.ProjectTask.Queries.GetPerformanceReport;

public class GetPerformanceReportQueryResponse
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public int TotalTasks { get; set; }
    public int TotalTasksCompleted { get; set; }
    public int TotalTasksPending { get; set; }
    public int TotalTasksInProgress { get; set; }
    public int TotalTasksOverdue { get; set; }
    public int TotalTasksCompletedOnTime { get; set; }
    public int TotalTasksCompletedLate { get; set; }
    public int TotalTasksCompletedEarly { get; set; }
    public DateTime ReferenceStartDate { get; set; }
    public DateTime ReferenceEndDate { get; set; }
}