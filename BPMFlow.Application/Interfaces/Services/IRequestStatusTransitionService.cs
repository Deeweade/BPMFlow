using BPMFlow.Application.Models.Views.BPMFlow;

namespace BPMFlow.Application.Interfaces.Services;

public interface IRequestStatusTransitionService
{
    Task<IEnumerable<RequestStatusTransitionView>> GetAvailableTransitionByUser(int code, string login);
}