using System.Linq.Expressions;
using BPMFlow.Domain.Dtos.Entities.BPMFlow;

namespace BPMFlow.Domain.Interfaces.Repositories;

public interface IRequestStatusTransitionRepository
{
    Task<RequestStatusTransitionDto> GetById(int requestStatusTransitionId);
    Task<RequestStatusTransitionDto> GetTransition(int sourceOrder, int nextOrder, int requestId);
    Task<IEnumerable<RequestStatusTransitionDto>> GetAvailableTransition(Expression<Func<RequestStatusTransitionDto, bool>> predicate);
}