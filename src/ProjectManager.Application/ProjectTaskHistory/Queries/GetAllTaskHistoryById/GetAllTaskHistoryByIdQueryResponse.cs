using ProjectManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectTaskHistory.Queries.GetAllTaskHistoryById
{
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
}
