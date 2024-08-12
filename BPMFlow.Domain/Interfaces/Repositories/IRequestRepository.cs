using BPMFlow.Domain.Dtos.Entities.BPMFlow;
using BPMFlow.Domain.Dtos.Filters;

namespace BPMFlow.Domain.Interfaces.Repositories;

public interface IRequestRepository
{
    Task<IEnumerable<RequestDto>> GetByFilter(RequestFilterDto filterDto);
}