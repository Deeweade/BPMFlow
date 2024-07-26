using System.Dynamic;
using BPMFlow.Domain.Dtos.Entities.BPMFlow;
using BPMFlow.Domain.Dtos.Filters;

namespace BPMFlow.Domain.Interfaces.Repositories;

public interface IObjectRequestRepository
{
    Task<IEnumerable<ObjectRequestDto>> GetBySystemObjectIdEmployee();
    Task<ObjectRequestDto> Create(ObjectRequestDto objectRequestDto);
    Task<IEnumerable<ObjectRequestDto>> GetBySystemObjectId();
    Task<IEnumerable<ObjectRequestDto>> GetByFilter(ObjectRequestsFilterDto filterDto);
    Task CloseRequest(ObjectRequestDto objectRequestDto);
    Task<IEnumerable<ObjectRequestDto>> GetParallelRequests(int code, int entityStatusId);
    Task AddObjectRequest(ObjectRequestDto objectRequestDto);
}