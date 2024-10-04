using ProjectManager.Domain.Entities;
using ProjectManager.Infrastructure.SQLServer.Contexts;
using ProjectManager.Infrastructure.SQLServer.Repositories.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace ProjectManager.Infrastructure.SQLServer.Repositories;
[ExcludeFromCodeCoverage]
public class CommentTaskRepository(ProjectManagerDbContext context)
    : BaseRepository<ProjectTaskComment>(context), ICommentTaskRepository
{
}