using BPFlow.Domain.Models.Entities.BPFlow;

namespace BPFlow.Domain.Models.Entities.PerfManagement1;

public class Role : BaseEntity
{
    public Role()
    {
        RoleAllowedActions = new HashSet<RoleAllowedAction>();
        EmployeeRoles = new HashSet<EmployeeRole>();
        RequestStatusTransitions = new HashSet<RequestStatusTransition>();
        RequestStatuses = new HashSet<RequestStatus>();
    }
    public string Title { get; set; }

    // Role -> RoleType
    public int? RoleTypeId { get; set; }
    public virtual RoleType? RoleType { get; set; }

    // Role -> RoleAllowedAction
    public virtual ICollection<RoleAllowedAction> RoleAllowedActions { get; set; }

    // Role -> EmployeeRole
    public virtual ICollection<EmployeeRole> EmployeeRoles { get; set; }

    // Role -> RequestStatusTransition
    public virtual ICollection<RequestStatusTransition> RequestStatusTransitions { get; set; }

    // Role -> RequestStatus
    public virtual ICollection<RequestStatus> RequestStatuses { get; set; }
}