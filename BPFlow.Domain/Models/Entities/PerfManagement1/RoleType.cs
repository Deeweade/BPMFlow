namespace BPFlow.Domain.Models.Entities.PerfManagement1;

public class RoleType : BaseEntity
{
    public RoleType()
    {
        Roles = new HashSet<Role>();
    }
    public string Name { get; set; }

    public virtual ICollection<Role> Roles { get; set; }
}