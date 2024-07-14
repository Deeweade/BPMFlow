using System.ComponentModel.DataAnnotations.Schema;

namespace BPMFlow.Domain.Models.Entities.PerfManagement1;

[Table("RoleAllowedAction")]
public class RoleAllowedAction : BaseEntity
{
    // RoleAllowedAction -> Action
    public int? ActionId { get; set; }
    public virtual Action Action { get; set; }

    // RoleAllowedAction -> Role
    public int? RoleId { get; set; }
    public virtual Role Role { get; set; }
}