namespace BPFlow.Domain.Models.Entities.PerfManagement1;

public class RoleAllowedAction : BaseEntity
{
    // RoleAllowedAction -> Action
    public int? ActionId { get; set; }
    public virtual Action? Actions { get; set; }

    // RoleAllowedAction -> Role
    public int? RoleId { get; set; }
    public virtual Role? Roles { get; set; }
}