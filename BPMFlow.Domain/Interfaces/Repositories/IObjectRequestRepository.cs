using System.Dynamic;
using BPMFlow.Domain.Dtos.Entities.BPMFlow;
using BPMFlow.Domain.Dtos.Filters;
using BPMFlow.Domain.Models.Entities.BPMFlow;

namespace BPMFlow.Domain.Interfaces.Repositories;

public interface IObjectRequestRepository
{
    Task<ObjectRequestDto> GetById(int requestId);
    Task<ObjectRequestDto> GetActiveByCode(int code);
    Task<IEnumerable<ObjectRequestDto>> GetByResponsibleEmployeeId(int employeeId);
    Task<IEnumerable<ObjectRequestDto>> GetBySystemObjectIdEmployee();
    Task<IEnumerable<ObjectRequestDto>> GetByFilter(ObjectRequestsFilterDto filterDto);
    Task<ObjectRequestDto> Create(ObjectRequestDto objectRequestDto);
    Task CloseRequest(ObjectRequestDto objectRequestDto);
    Task<IEnumerable<ObjectRequestDto>> GetParallelRequests(int code, int entityStatusId);
    Task AddObjectRequest(ObjectRequestDto objectRequestDto);
    Task<IEnumerable<ObjectRequestDto>> ChangeResponsibleEmployee(int[] requestCodes, int newResponsibleEmployeeId);
}