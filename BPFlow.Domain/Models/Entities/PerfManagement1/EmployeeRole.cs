namespace BPFlow.Domain.Models.Entities.PerfManagement1;

public class EmployeeRole : BaseEntity
{
    public EmployeeRole()
    {
        
    }

    // EmployeeRole -> Employee
    public int? EmployeeId { get; set; }
    public virtual Employee? Employees { get; set; }

    // EmployeeRole -> Role
    public int? RoleId { get; set; }
    public virtual Role? Roles { get; set; }
}