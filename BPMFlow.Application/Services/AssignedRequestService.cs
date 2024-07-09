using System.Security.Cryptography;
using AutoMapper;
using BPMFlow.Application.Interfaces.Services;
using BPMFlow.Application.Models.Views.BPMFlow;
using BPMFlow.Domain.Dtos.Entities.BPMFlow;
using BPMFlow.Domain.Interfaces.Repositories;

namespace BPMFlow.Application.Services;

public class AssignedRequestService : IAssignedRequestService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AssignedRequestService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AssignedRequestView?> Create(AssignedRequestView assignedRequestView)
    {
        if (assignedRequestView is null) throw new ArgumentNullException(nameof(assignedRequestView));

        var assignedRequestDto = _mapper.Map<AssignedRequestDto>(assignedRequestView);

        var assignedRequest = await _unitOfWork.AssignedRequestRepository.Create(assignedRequestDto);

        return _mapper.Map<AssignedRequestView>(assignedRequest);
    }
}