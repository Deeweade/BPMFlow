using BPMFlow.Application.Models.Views.BPMFlow;
using BPMFlow.Application.Models.Filters;

namespace BPMFlow.Application.Interfaces.Services;

public interface IObjectRequestService
{
    //Task<ObjectRequestView> Create(ObjectRequestView objectRequestView);
    //Task<IEnumerable<int>> BulkCreate(ICollection<int> employeeIds, ObjectRequestView objectRequests);
    //Task<IEnumerable<ObjectRequestView>> GetByFilter(ObjectRequestsFilterView filterView);
    Task<ObjectRequestView> ChangeStatus(ObjectRequestView objectRequestView, int nextStatusOrder);
}