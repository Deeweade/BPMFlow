using AutoMapper;
using BPMFlow.Application.Interfaces.Services;
using BPMFlow.Application.Models.Filters;
using BPMFlow.Application.Models.Views.BPMFlow;
using BPMFlow.Domain.Dtos.Filters;
using BPMFlow.Domain.Interfaces.Repositories;

namespace BPMFlow.Application.Services;

public class RequestService(IUnitOfWork unitOfWork, IMapper mapper) : IRequestService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<RequestView>> GetByFilter(RequestFilterView filterView)
    {
        ArgumentNullException.ThrowIfNull(filterView);

        var filterDto = _mapper.Map<RequestFilterDto>(filterView);

        if (filterDto.WithSubordinates && filterDto.EmployeeId.HasValue)
        {
            var requestByEmployee = await _unitOfWork.ObjectRequestRepository.GetBySystemObjectIdEmployee();

            if (requestByEmployee.Any())
            {
                var employeeIds = (await _unitOfWork.EmployeeRepository.GetSubordinateEmployeeIds(filterDto.EmployeeId!.Value)).ToList();

                filterDto.SubordinateEmployeeIds = employeeIds;
                
                filterDto.SubordinateEmployeeIds.Add(filterDto.EmployeeId!.Value);
            }
        }

        var requests = await _unitOfWork.RequestRepository.GetByFilter(filterDto);

        return _mapper.Map<IEnumerable<RequestView>>(requests);
    }
}