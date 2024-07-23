using System.Dynamic;
using BPMFlow.Domain.Dtos.Entities.BPMFlow;
using BPMFlow.Domain.Dtos.Filters;

namespace BPMFlow.Domain.Interfaces.Repositories;

public interface IObjectRequestRepository
{
    Task<ObjectRequestDto> Create(ObjectRequestDto objectRequestDto);
    Task<IEnumerable<ObjectRequestDto>> GetBySystemObjectId();
    Task<IEnumerable<ObjectRequestDto>> GetByFilter(ObjectRequestsFilterDto filterDto);
    Task<ObjectRequestDto> ChangeStatus(ObjectRequestDto objectRequestDto, int nextStatusOrder);
}