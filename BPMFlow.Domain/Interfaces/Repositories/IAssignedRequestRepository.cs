using System.Dynamic;
using BPMFlow.Domain.Dtos.Entities.BPMFlow;
using BPMFlow.Domain.Dtos.Filters;

namespace BPMFlow.Domain.Interfaces.Repositories;

public interface IAssignedRequestRepository
{
    Task<AssignedRequestDto?> Create(AssignedRequestDto assignedRequestDto);
    Task<IEnumerable<AssignedRequestDto>> GetByFilter(AssignedRequestsFilterDto filterDto);
}