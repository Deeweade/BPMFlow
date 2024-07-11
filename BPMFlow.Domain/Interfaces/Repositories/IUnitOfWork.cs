namespace BPMFlow.Domain.Interfaces.Repositories;

public interface IUnitOfWork
{
    IAssignedRequestRepository AssignedRequestRepository { get; }

    Task<int> SaveChangesAsync();
}