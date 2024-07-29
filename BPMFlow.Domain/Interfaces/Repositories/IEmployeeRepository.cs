using BPMFlow.Domain.Dtos.Entities.PerfManagement1;

namespace BPMFlow.Domain.Interfaces.Repositories;

public interface IEmployeeRepository
{
    Task<EmployeeDto> GetById(int employeeId);
    Task<IEnumerable<int>> GetSubordinateEmployeeIds(int employeeId);
    Task<int> GetResponsibleEmployeeId(int responsibleRoleId);
    Task<EmployeeDto> GetByUserLogin(string login);
}