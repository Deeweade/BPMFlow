using BPMFlow.Domain.Dtos.Entities.BPMFlow;

namespace BPMFlow.Domain.Interfaces.Repositories;

public interface IRequestStatusRepository
{
    Task<RequestStatusDto> GetById(int requestStatusId);
    Task<IEnumerable<RequestStatusDto>> GetStatusesByRequestId(int requestId);
    Task<IEnumerable<RequestStatusDto>> GetByOrderAndRequestId(int order, int requestId);
    Task<int> GetResponsibleRoleIdByStatusId(int requestStatusId);
    Task<IEnumerable<RequestStatusDto>> GetStatusesByCode(int requestStatusId);
    Task<int> GetRequestId(int requestStatusId);
}