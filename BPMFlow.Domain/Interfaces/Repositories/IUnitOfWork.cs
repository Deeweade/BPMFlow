namespace BPMFlow.Domain.Interfaces.Repositories;

public interface IUnitOfWork
{
    IObjectRequestRepository ObjectRequestRepository { get; }
    IPeriodRepository PeriodRepository { get; }
    IEmployeeRepository EmployeeRepository { get; }
    IRequestStatusRepository RequestStatusRepository { get; }
    IRequestStatusTransitionRepository RequestStatusTransitionRepository { get; }
    
    Task<int> SaveChangesAsync();
}