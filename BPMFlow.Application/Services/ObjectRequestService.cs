using AutoMapper;
using BPMFlow.Application.Interfaces.Services;
using BPMFlow.Application.Models.Filters;
using BPMFlow.Application.Models.Views.BPMFlow;
using BPMFlow.Domain.Dtos.Entities.BPMFlow;
using BPMFlow.Domain.Dtos.Filters;
using BPMFlow.Domain.Interfaces.Repositories;
using BPMFlow.Domain.Models.Enums;

namespace BPMFlow.Application.Services;

public class ObjectRequestService : IObjectRequestService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ObjectRequestService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ObjectRequestView> Create(ObjectRequestView objectRequestView)
    {
        ArgumentNullException.ThrowIfNull(objectRequestView);


        var objectRequestDto = _mapper.Map<ObjectRequestDto>(objectRequestView);

        var objectRequest = await _unitOfWork.ObjectRequestRepository.Create(objectRequestDto);

        return _mapper.Map<ObjectRequestView>(objectRequest);
    }

    public async Task<IEnumerable<int>> BulkCreate(ICollection<int> objectIds, ObjectRequestView objectRequests)
    {
        ArgumentNullException.ThrowIfNull(objectIds);
        ArgumentNullException.ThrowIfNull(objectRequests);

        var codes = new List<int>();

        foreach (var objectId in objectIds)
        {
            var newRequest = new ObjectRequestView
                {
                    RequestStatusId = objectRequests.RequestStatusId,
                    ResponsibleEmployeeId = objectRequests.ResponsibleEmployeeId,
                    ObjectId = objectId,
                    PeriodId = objectRequests.PeriodId,
                    EntityStatusId = objectRequests.EntityStatusId
                };

            var createdRequest = await Create(newRequest);
            
            if (createdRequest != null)
            {
                codes.Add(createdRequest.Code);
            }
        }

        return codes;
    }

    public async Task<IEnumerable<ObjectRequestView>> GetByFilter(ObjectRequestsFilterView filterView)
    {
        ArgumentNullException.ThrowIfNull(filterView);

        var filterDto = _mapper.Map<ObjectRequestsFilterDto>(filterView);

        if (filterDto.PeriodId.HasValue && filterDto.PeriodId.Value != 0)
        {
            var period =  await _unitOfWork.PeriodRepository.GetById(filterDto.PeriodId!.Value);
            
            if (period is not null && period.IsYear == 1)
            {
                var childPeriodIds = (await _unitOfWork.PeriodRepository.GetChildPeriodIds(filterDto.PeriodId.Value)).ToList();

                filterDto.PeriodIds = childPeriodIds;
                
                filterDto.PeriodIds.Add(filterDto.PeriodId.Value);
            }
        }

        if (filterDto.WithSubordinates)
        {
            var requestByEmployee = await _unitOfWork.ObjectRequestRepository.GetBySystemObjectId();

            if (requestByEmployee.Any())
            {
                var objectIds = (await _unitOfWork.EmployeeRepository.GetSubordinateEmployeeIds(filterDto.ObjectId!.Value)).ToList();

                filterDto.SubordinateEmployeeIds = objectIds;
                
                filterDto.SubordinateEmployeeIds.Add(filterDto.ObjectId!.Value);
            }
        }

        var queries = await _unitOfWork.ObjectRequestRepository.GetByFilter(filterDto);

        return _mapper.Map<IEnumerable<ObjectRequestView>>(queries);
    }
}