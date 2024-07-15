using System.ComponentModel.DataAnnotations.Schema;
using BPMFlow.Domain.Models.Entities.BPMFlow;

namespace BPMFlow.Domain.Models.Entities.PerfManagement1;

[Table("Role")]
public class Role : BaseEntity
{
    public Role()
    {
        RoleAllowedActions = new HashSet<RoleAllowedAction>();
        EmployeeRoles = new HashSet<EmployeeRole>();
    }
    public string Title { get; set; }

    // Role -> RoleType
    public int? RoleTypeId { get; set; }
    public virtual RoleType RoleType { get; set; }

    // Role -> RoleAllowedAction
    public virtual ICollection<RoleAllowedAction> RoleAllowedActions { get; set; }

    // Role -> EmployeeRole
    public virtual ICollection<EmployeeRole> EmployeeRoles { get; set; }
}