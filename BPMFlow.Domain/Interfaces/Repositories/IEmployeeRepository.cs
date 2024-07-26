namespace BPMFlow.Domain.Interfaces.Repositories;

public interface IEmployeeRepository
{
    Task<IEnumerable<int>> GetSubordinateEmployeeIds(int employeeId);
    Task<int> GetResponsibleEmployeeId(int responsibleRoleId);
}