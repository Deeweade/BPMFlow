using BPMFlow.Domain.Dtos.Entities.BPMFlow;

namespace BPMFlow.Domain.Interfaces.Repositories;

public interface IAssignedRequestRepository
{
    Task<AssignedRequestDto> Create(AssignedRequestDto assignedRequestDto);
}