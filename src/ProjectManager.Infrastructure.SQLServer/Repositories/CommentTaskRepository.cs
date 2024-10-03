using ProjectManager.Domain.Entities;
using ProjectManager.Infrastructure.SQLServer.Context;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;

namespace ProjectManager.Infrastructure.SQLServer.Repositories;

public class CommentTaskRepository(ProjectManagerDbContext context) :BaseRepository<ProjectTaskComment>(context), ICommentTaskRepository
{
    
}