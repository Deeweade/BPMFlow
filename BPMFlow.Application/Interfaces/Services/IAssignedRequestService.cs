using BPMFlow.Application.Models.Views.BPMFlow;
using BPMFlow.Application.Models.Filters;

namespace BPMFlow.Application.Interfaces.Services;

public interface IAssignedRequestService
{
    Task<AssignedRequestView> Create(AssignedRequestView assignedRequestView);
    Task<IEnumerable<AssignedRequestView>> GetByFilter(AssignedRequestsFilterView filterView);
}