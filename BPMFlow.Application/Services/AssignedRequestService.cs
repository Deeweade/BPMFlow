using System.Linq;
using System.Security.Permissions;
using AutoMapper;
using BPMFlow.Application.Interfaces.Services;
using BPMFlow.Application.Models.Filters;
using BPMFlow.Application.Models.Views.BPMFlow;
using BPMFlow.Domain.Dtos.Entities.BPMFlow;
using BPMFlow.Domain.Dtos.Filters;
using BPMFlow.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

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

    public async Task<IEnumerable<int>> BulkCreate(ICollection<int> employeeIds, ICollection<AssignedRequestView> assignedRequests)
    {
        if (employeeIds is null) throw new ArgumentNullException(nameof(employeeIds));
        if (assignedRequests is null) throw new ArgumentNullException(nameof(assignedRequests));

        var codes = new List<int>();

        foreach (var employeeId in employeeIds)
        {
            foreach (var assignedRequest in assignedRequests)
            {
                var newRequest = new AssignedRequestView
                    {
                        GroupRequestId = assignedRequest.GroupRequestId,
                        RequestStatusId = assignedRequest.RequestStatusId,
                        ResponsibleEmployeeId = assignedRequest.ResponsibleEmployeeId,
                        EmployeeId = employeeId,
                        PeriodId = assignedRequest.PeriodId,
                        EntityStatusId = assignedRequest.EntityStatusId
                    };

                    var createdRequest = await Create(newRequest);
                    
                    if (createdRequest != null)
                    {
                        codes.Add(createdRequest.Code);
                    }
                // assignedRequest.EmployeeId = employeeId;

                // var createdRequest = await Create(assignedRequest);
                
                // if (createdRequest != null)
                // {
                //     codes.Add(createdRequest.Code);
                // }
            }
        }

        return codes;
    }

    public async Task<IEnumerable<AssignedRequestView>> GetByFilter(AssignedRequestsFilterView filterView)
    {
        if (filterView is null) throw new ArgumentNullException(nameof(filterView));

        var filterDto = _mapper.Map<AssignedRequestsFilterDto>(filterView);

        // фильтруем
        var query = (await _unitOfWork.AssignedRequestRepository.GetByFilter(filterDto)).AsQueryable();

        // дочерние периоды
        if (filterDto.PeriodId.HasValue)
        {
            // получаем период
            var period =  await _unitOfWork.PeriodRepository.GetById(filterDto.PeriodId.Value);

            if (period is not null && period.IsYear == 1)
            {
                // получаем дочерние
                var childPeriodIds = (await _unitOfWork.PeriodRepository.GetChildPeriodIds(filterDto.PeriodId.Value)).ToList();

                childPeriodIds.Add(filterDto.PeriodId.Value);

                query = query.Where(x => childPeriodIds.Contains((int)x.PeriodId!));
            }
        }

        if (filterDto.WithSubordinates) 
        {
            // получаем сотрудников
            var employeeIds = (await _unitOfWork.EmployeeRepository.GetSubordinateEmployeeIds(filterDto.EmployeeId!.Value)).ToList();
            
            employeeIds.Add(filterDto.EmployeeId.Value);

            query = query.Where(x => employeeIds.Contains((int)x.EmployeeId!) || x.ResponsibleEmployeeId == filterDto.EmployeeId.Value);
        }
        else
            query = query.Where(x => x.EmployeeId == filterDto.EmployeeId.Value);

        var queries = await query.Distinct().ToListAsync();

        var views = _mapper.Map<IEnumerable<AssignedRequestView>>(queries);

        return views;
    }
}