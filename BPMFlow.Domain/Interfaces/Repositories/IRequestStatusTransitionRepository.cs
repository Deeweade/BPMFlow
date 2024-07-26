using BPMFlow.Domain.Dtos.Entities.BPMFlow;

namespace BPMFlow.Domain.Interfaces.Repositories;

public interface IRequestStatusTransitionRepository
{
    Task<RequestStatusTransitionDto> GetTransition(int sourceOrder, int nextOrder, int requestId);
    Task<IEnumerable<RequestStatusTransitionDto>> GetAvailableTransitionByUser(int code, string login);
}