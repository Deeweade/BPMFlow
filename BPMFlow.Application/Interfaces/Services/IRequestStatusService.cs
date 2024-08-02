using BPMFlow.Application.Models.Views.BPMFlow;
using BPMFlow.Application.Models.Filters;

namespace BPMFlow.Application.Interfaces.Services;

public interface IRequestStatusService
{
    Task<IEnumerable<RequestStatusView>> GetStatusByRequest(int requestId);
}