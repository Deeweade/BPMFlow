using BPMFlow.Application.Models.Views.BPMFlow;
using BPMFlow.Application.Models.Filters;

namespace BPMFlow.Application.Interfaces.Services;

public interface IObjectRequestService
{
    Task<ObjectRequestView> Create(ObjectRequestView objectRequestView, string login);
    Task<IEnumerable<int>> BulkCreate(ICollection<int> employeeIds, ObjectRequestView objectRequests, string login);
    Task<ObjectRequestView> GetActiveByCode(int code);
    Task<IEnumerable<ObjectRequestView>> GetByFilter(ObjectRequestsFilterView filterView);
    Task<ObjectRequestView> ChangeStatus(ObjectRequestView objectRequestView, int nextStatusOrder);
}