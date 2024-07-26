using System.Dynamic;
using BPMFlow.Domain.Dtos.Entities.BPMFlow;
using BPMFlow.Domain.Dtos.Filters;

namespace BPMFlow.Domain.Interfaces.Repositories;

public interface IObjectRequestRepository
{
    Task<IEnumerable<ObjectRequestDto>> GetBySystemObjectIdEmployee();
    Task<ObjectRequestDto> Create(ObjectRequestDto objectRequestDto);
    Task<IEnumerable<ObjectRequestDto>> GetByFilter(ObjectRequestsFilterDto filterDto);
}