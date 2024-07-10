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

    public async Task<AssignedRequestView?> Create(AssignedRequestView assignedRequestView)
    {
        if (assignedRequestView is null) throw new ArgumentNullException(nameof(assignedRequestView));

        var assignedRequestDto = _mapper.Map<AssignedRequestDto>(assignedRequestView);

        var assignedRequest = await _unitOfWork.AssignedRequestRepository.Create(assignedRequestDto);

        return _mapper.Map<AssignedRequestView>(assignedRequest);
    }

    public async Task<IEnumerable<AssignedRequestView>> GetByFilter(AssignedRequestsFilterView filterView)
    {
        if (filterView is null) throw new ArgumentNullException(nameof(filterView));

        var filterDto = _mapper.Map<AssignedRequestsFilterDto>(filterView);

        // фильтруем
        var query = (await _unitOfWork.AssignedRequestRepository.GetByFilter(filterDto)).ToList();

        // дочерние периоды
        if (filterDto.PeriodId.HasValue)
        {
            // получаем период
            var period =  await _unitOfWork.PeriodRepository.GetById(filterDto.PeriodId.Value);

            if (period is not null && period.IsYear == 1)
            {
                // получаем дочерние
                var childPeriodIds = (await _unitOfWork.PeriodRepository.GetChildPeriodIds(filterDto.PeriodId.Value)).ToList();

                query = query.Where(x => childPeriodIds.Contains((int)x.PeriodId!) || x.PeriodId == filterDto.PeriodId.Value).ToList();
            }
        }

        if (filterDto.WithSubordinates) 
        {
            // получаем сотрудников
            var employeeIds = (await _unitOfWork.EmployeeRepository.GetSubordinateEmployeeIds(filterDto.EmployeeId!.Value)).ToList();
            
            employeeIds.Add(filterDto.EmployeeId.Value);

            query = query.Where(x => employeeIds.Contains((int)x.EmployeeId!) || x.ResponsibleEmployeeId == filterDto.EmployeeId.Value).ToList();
        }
        else
            query = query.Where(x => x.EmployeeId == filterDto.EmployeeId.Value).ToList();

        var queries = query.Distinct().ToList();

        var views = _mapper.Map<IEnumerable<AssignedRequestView>>(queries);

        return views;
    }
}