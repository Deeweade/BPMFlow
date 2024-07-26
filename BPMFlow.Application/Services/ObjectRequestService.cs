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

    public async Task<ObjectRequestView> ChangeStatus(ObjectRequestView objectRequestView, int nextStatusOrder)
    {
        ArgumentNullException.ThrowIfNull(objectRequestView);
        
        var objectRequestDto = _mapper.Map<ObjectRequestDto>(objectRequestView);

        var objectRequest = await _unitOfWork.ObjectRequestRepository.GetById(objectRequestDto.Id); 
        ArgumentNullException.ThrowIfNull(objectRequest);

        var currentStatus = await _unitOfWork.RequestStatusRepository.GetById(objectRequest.RequestStatusId);
        ArgumentNullException.ThrowIfNull(currentStatus);

        var transition = await _unitOfWork.RequestStatusTransitionRepository.GetTransition(currentStatus.StatusOrder, nextStatusOrder, currentStatus.RequestId);
        ArgumentNullException.ThrowIfNull(transition);

        // close current ObjectRequest
        objectRequest.EntityStatusId = 2;
        objectRequest.DateEnd = DateTime.Now;
        objectRequest.RequestStatusTransitionId = transition.Id;
        await _unitOfWork.ObjectRequestRepository.CloseRequest(objectRequest);

        var parallelRequests = await _unitOfWork.ObjectRequestRepository.GetParallelRequests(objectRequest.Code, 1);
        
        if (!parallelRequests.Any())
        {
            // no parallel statuses then move to next status
            var nextStatuses = await _unitOfWork.RequestStatusRepository.GetByOrderAndRequestId(nextStatusOrder, currentStatus.RequestId);
            
            foreach (var nextStatus in nextStatuses)
            {
                var newObjectRequest = new ObjectRequestDto
                {
                    ObjectId = objectRequest.ObjectId,
                    AuthorEmployeeId = objectRequest.AuthorEmployeeId,
                    ResponsibleEmployeeId = await _unitOfWork.EmployeeRepository.GetResponsibleEmployeeId(nextStatus.ResponsibleRoleId),
                    RequestStatusId = nextStatus.Id,
                    EntityStatusId = 1,
                    DateStart = DateTime.Now,
                    DateEnd = DateTime.MaxValue
                };

                await _unitOfWork.ObjectRequestRepository.AddObjectRequest(newObjectRequest);
            }
        }
        else
        {
            if (transition.SourceStatusOrder > transition.NextStatusOrder)
            {
                // move backward
                foreach (var parallelRequest in parallelRequests)
                {
                    parallelRequest.EntityStatusId = 2;
                    parallelRequest.DateEnd = DateTime.UtcNow;
                    
                    await _unitOfWork.ObjectRequestRepository.AddObjectRequest(parallelRequest);
                }

                var nextStatuses = await _unitOfWork.RequestStatusRepository.GetByOrderAndRequestId(nextStatusOrder, currentStatus.RequestId);
                
                foreach (var nextStatus in nextStatuses)
                {
                    var newObjectRequest = new ObjectRequestDto
                    {
                        ObjectId = objectRequest.ObjectId,
                        AuthorEmployeeId = objectRequest.AuthorEmployeeId,
                        ResponsibleEmployeeId = await _unitOfWork.EmployeeRepository.GetResponsibleEmployeeId(nextStatus.ResponsibleRoleId),
                        RequestStatusId = nextStatus.Id,
                        EntityStatusId = 1,
                        DateStart = DateTime.Now,
                        DateEnd = DateTime.MaxValue
                    };

                    await _unitOfWork.ObjectRequestRepository.AddObjectRequest(newObjectRequest);
                }
            }
            else if (transition.SourceStatusOrder < transition.NextStatusOrder && currentStatus.IsFinalDenied)
            {
                // move forward and final denied
                foreach (var parallelRequest in parallelRequests)
                {
                    parallelRequest.EntityStatusId = 2;
                    parallelRequest.DateEnd = DateTime.Now;

                    await _unitOfWork.ObjectRequestRepository.AddObjectRequest(parallelRequest);
                }

                var newObjectRequest = new ObjectRequestDto
                {
                    ObjectId = objectRequest.ObjectId,
                    AuthorEmployeeId = objectRequest.AuthorEmployeeId,
                    ResponsibleEmployeeId = await _unitOfWork.EmployeeRepository.GetResponsibleEmployeeId(currentStatus.ResponsibleRoleId),
                    RequestStatusId = currentStatus.Id,
                    EntityStatusId = 3,
                    DateStart = DateTime.UtcNow,
                    DateEnd = DateTime.MaxValue
                };

                await _unitOfWork.ObjectRequestRepository.AddObjectRequest(newObjectRequest);
            }
        }

        return _mapper.Map<ObjectRequestView>(objectRequest);
    }
}