using System.ComponentModel.DataAnnotations.Schema;

namespace BPMFlow.Domain.Models.Entities.PerfManagement1;

[Table("RoleType")]
public class RoleType : BaseEntity
{
    public RoleType()
    {
        Roles = new HashSet<Role>();
    }
    public string Name { get; set; }

    public virtual ICollection<Role> Roles { get; set; }
}