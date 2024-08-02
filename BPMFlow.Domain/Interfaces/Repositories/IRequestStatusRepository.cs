using BPMFlow.Domain.Dtos.Entities.BPMFlow;

namespace BPMFlow.Domain.Interfaces.Repositories;

public interface IRequestStatusRepository
{
    Task<RequestStatusDto> GetById(int requestStatusId);
    Task<IEnumerable<RequestStatusDto>> GetByOrderAndRequestId(int order, int requestId);
    Task<IEnumerable<RequestStatusDto>> GetByRequestId(int requestId);
    Task<int> GetResponsibleRoleIdByStatusId(int requestStatusId);
}