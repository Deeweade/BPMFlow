namespace BPMFlow.Domain.Interfaces.Repositories;

public interface IUnitOfWork
{
    IAssignedRequestRepository AssignedRequestRepository { get; }
    IPeriodRepository PeriodRepository { get; }
    IEmployeeRepository EmployeeRepository { get; }
    
    Task<int> SaveChangesAsync();
}