using AutoMapper;
using BPMFlow.Application.Interfaces.Services;
using BPMFlow.Application.Models.Views.BPMFlow;
using BPMFlow.Domain.Dtos.Entities.BPMFlow;
using BPMFlow.Domain.Interfaces.Repositories;

namespace BPMFlow.Application.Services;

public class RequestStatusTransitionService : IRequestStatusTransitionService
{
    private readonly IRequestStatusTransitionService _service;

    public RequestStatusTransitionService(IRequestStatusTransitionService service)
    {
        _service = service;
    }

    public async Task<IEnumerable<RequestStatusTransitionView>> GetAvailableTransitionByUser(int code)
    {
        ArgumentNullException.ThrowIfNull(code);
        
        var transitions = await _service.GetAvailableTransitionByUser(code);

        return transitions;
    }
}