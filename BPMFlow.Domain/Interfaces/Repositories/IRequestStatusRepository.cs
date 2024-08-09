using BPMFlow.Domain.Dtos.Entities.BPMFlow;

namespace BPMFlow.Domain.Interfaces.Repositories;

public interface IRequestStatusRepository
{
    Task<RequestStatusDto> GetById(int requestStatusId);
    Task<IEnumerable<RequestStatusDto>> GetStatusesByRequestStatusId(int requestId);
    Task<IEnumerable<RequestStatusDto>> GetByOrderAndRequestId(int order, int requestStatusId);
    Task<int> GetResponsibleRoleIdByStatusId(int requestStatusId);
    Task<IEnumerable<RequestStatusDto>> GetStatusesByCode(int requestStatusId);
}