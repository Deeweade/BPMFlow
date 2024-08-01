using AutoMapper;
using BPMFlow.Application.Interfaces.Services;
using BPMFlow.Application.Models.Views.BPMFlow;
using BPMFlow.Domain.Interfaces.Repositories;

namespace BPMFlow.Application.Services;

public class RequestStatusService : IRequestStatusService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RequestStatusService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RequestStatusView>> GetStatusByRequest(int requestId)
    {
        ArgumentNullException.ThrowIfNull(requestId);

        var requestStatuses = await _unitOfWork.RequestStatusRepository.GetStatusByRequest(requestId);

        return _mapper.Map<IEnumerable<RequestStatusView>>(requestStatuses);
    }
}