using BPMFlow.Application.Models.Views.BPMFlow;
using BPMFlow.Application.Models.Filters;

namespace BPMFlow.Application.Interfaces.Services;

public interface IObjectRequestService
{
    /// <param name="authorLogin"> employee's login who is the author of the request </param>

    Task<ObjectRequestView> GetActiveByCode(int code);
    Task<IEnumerable<ObjectRequestView>> GetResponsibleByLogin(string login);
    Task<ObjectRequestView> Create(ObjectRequestView objectRequestView, string authorLogin);
    Task<IEnumerable<int>> BulkCreate(ICollection<int> employeeIds, ObjectRequestView objectRequests, string authorLogin);
    Task<IEnumerable<ObjectRequestView>> GetByFilter(ObjectRequestsFilterView filterView);
    Task<ObjectRequestView> ChangeStatus(ObjectRequestView objectRequestView, int nextStatusOrder);
}