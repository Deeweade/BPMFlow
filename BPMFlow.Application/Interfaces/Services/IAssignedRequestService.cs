using BPMFlow.Application.Models.Views.BPMFlow;
using BPMFlow.Application.Models.Filters;
using BPMFlow.Domain.Models.Entities.BPMFlow;

namespace BPMFlow.Application.Interfaces.Services;

public interface IAssignedRequestService
{
    Task<AssignedRequestView?> Create(AssignedRequestView assignedRequestView);
    Task<IEnumerable<int>> BulkCreate(ICollection<int> employeeIds, ICollection<AssignedRequestView> assignedRequests);
    Task<IEnumerable<AssignedRequestView>> GetByFilter(AssignedRequestsFilterView filterView);
}