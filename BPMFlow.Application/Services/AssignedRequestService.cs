using AutoMapper;
using BPMFlow.Application.Interfaces.Services;
using BPMFlow.Application.Models.Filters;
using BPMFlow.Application.Models.Views.BPMFlow;
using BPMFlow.Domain.Dtos.Entities.BPMFlow;
using BPMFlow.Domain.Dtos.Filters;
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

    public async Task<AssignedRequestView> Create(AssignedRequestView assignedRequestView)
    {
        ArgumentNullException.ThrowIfNull(assignedRequestView);


        var assignedRequestDto = _mapper.Map<AssignedRequestDto>(assignedRequestView);

        var assignedRequest = await _unitOfWork.AssignedRequestRepository.Create(assignedRequestDto);

        return _mapper.Map<AssignedRequestView>(assignedRequest);
    }

    public async Task<IEnumerable<int>> BulkCreate(ICollection<int> employeeIds, AssignedRequestView assignedRequests)
    {
        ArgumentNullException.ThrowIfNull(employeeIds);
        ArgumentNullException.ThrowIfNull(assignedRequests);

        var codes = new List<int>();

        foreach (var employeeId in employeeIds)
        {
            var newRequest = new AssignedRequestView
                {
                    GroupRequestId = assignedRequests.GroupRequestId,
                    RequestStatusId = assignedRequests.RequestStatusId,
                    ResponsibleEmployeeId = assignedRequests.ResponsibleEmployeeId,
                    EmployeeId = employeeId,
                    PeriodId = assignedRequests.PeriodId,
                    EntityStatusId = assignedRequests.EntityStatusId
                };

            var createdRequest = await Create(newRequest);
            
            if (createdRequest != null)
            {
                codes.Add(createdRequest.Code);
            }
        }

        return codes;
    }

    public async Task<IEnumerable<AssignedRequestView>> GetByFilter(AssignedRequestsFilterView filterView)
    {
        ArgumentNullException.ThrowIfNull(filterView);

        var filterDto = _mapper.Map<AssignedRequestsFilterDto>(filterView);

        if (filterDto.PeriodIds is not null && filterDto.PeriodIds.Any())
        {
            var period =  await _unitOfWork.PeriodRepository.GetById(filterDto.PeriodId!.Value);
            
            if (period is not null && period.IsYear == 1)
            {
                var childPeriodIds = (await _unitOfWork.PeriodRepository.GetChildPeriodIds(filterDto.PeriodId.Value)).ToList();

                filterDto.PeriodIds = childPeriodIds;
            }
            
            filterDto.PeriodIds.Add(filterDto.PeriodId.Value);
        }

        if (filterDto.WithSubordinates)
        {
            var employeeIds = (await _unitOfWork.EmployeeRepository.GetSubordinateEmployeeIds(filterDto.EmployeeId!.Value)).ToList();

            filterDto.SubordinateEmployeeIds = employeeIds;
        }

        filterDto.SubordinateEmployeeIds.Add(filterDto.EmployeeId!.Value);

        var queries = await _unitOfWork.AssignedRequestRepository.GetByFilter(filterDto);

        return _mapper.Map<IEnumerable<AssignedRequestView>>(queries);
    }
}