using System.ComponentModel.DataAnnotations.Schema;

namespace BPMFlow.Domain.Models.Entities.PerfManagement1;

[Table("EmployeeRole")]
public class EmployeeRole : BaseEntity
{
    public EmployeeRole()
    {
        
    }

    // EmployeeRole -> Employee
    public int EmployeeId { get; set; }
    public virtual Employee? Employee { get; set; }

    // EmployeeRole -> Role
    public int RoleId { get; set; }
    public virtual Role? Role { get; set; }
}