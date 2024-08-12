using BPMFlow.Application.Models.Filters;
using BPMFlow.Application.Models.Views.BPMFlow;

namespace BPMFlow.Application.Interfaces.Services;

public interface IRequestService
{
    Task<IEnumerable<RequestView>> GetByFilter(RequestFilterView requestFilterView);
}