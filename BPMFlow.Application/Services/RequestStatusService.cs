using AutoMapper;
using BPMFlow.Application.Interfaces.Services;
using BPMFlow.Application.Models.Views.BPMFlow;
using BPMFlow.Domain.Dtos.Entities.BPMFlow;
using BPMFlow.Domain.Interfaces.Repositories;

namespace BPMFlow.Application.Services;

public class RequestStatusService(IUnitOfWork unitOfWork, IMapper mapper) : IRequestStatusService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<RequestStatusView>> GetStatusesByRequestId(int requestId)
    {
        ArgumentNullException.ThrowIfNull(requestId);

        var requestStatuses = await _unitOfWork.RequestStatusRepository.GetStatusesByRequestId(requestId);

        return _mapper.Map<IEnumerable<RequestStatusView>>(requestStatuses);
    }

    public async Task<IEnumerable<RequestStatusView>> GetStatusesByCode(int code)
    {
        var activeRequest = await _unitOfWork.ObjectRequestRepository.GetActiveByCode(code);

        var parallelRequests = await _unitOfWork.ObjectRequestRepository.GetParallelRequests(activeRequest.Code, activeRequest.EntityStatusId);

        var allStatuses = new List<RequestStatusDto>();

        foreach (var parallelRequest in parallelRequests)
        {
            var status = await _unitOfWork.RequestStatusRepository.GetStatusesByRequestStatusId((int)parallelRequest.RequestStatusId);

            allStatuses.Add(status);
        }

        return _mapper.Map<IEnumerable<RequestStatusView>>(allStatuses);
    }
}