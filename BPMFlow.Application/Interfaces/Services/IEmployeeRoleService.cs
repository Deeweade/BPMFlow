using BPMFlow.Domain.Models.Enums;

namespace BPMFlow.Application.Interfaces.Services;

public interface IEmployeeRoleService
{
    Task<Roles> GetRoleInRequest(int currentEmployeeId, int employeeId, int employeeRequestCode);
    Task<Roles> GetRoleInOrgStructure(int currentEmployeeId, int employeeId);
}