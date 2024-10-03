using ProjectManager.Domain.Enums;

namespace ProjectManager.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string Document { get; set; }
    public List<EUserRole> Roles { get; set; }


    public IEnumerable<Project> Projects { get; set; }
}