using BPMFlow.Application.Models.Views.BPMFlow;

namespace BPMFlow.Application.Interfaces.Services;

public interface IAssignedRequestService
{
    Task<AssignedRequestView> Create(AssignedRequestView assignedRequestView);
}