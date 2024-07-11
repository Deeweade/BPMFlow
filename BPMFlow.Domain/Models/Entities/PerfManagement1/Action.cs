using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BPMFlow.Domain.Models.Entities.PerfManagement1;

[Table("Action")]
public class Action : BaseEntity
{
    public Action()
    {
        RoleAllowedActions = new HashSet<RoleAllowedAction>();
    }

    public string Name { get; set; }

    // Action -> RoleAllowedAction
    public virtual ICollection<RoleAllowedAction> RoleAllowedActions { get; set; }
}