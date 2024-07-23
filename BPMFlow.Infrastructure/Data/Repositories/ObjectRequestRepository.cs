using System.Reflection;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BPMFlow.Domain.Dtos.Entities.BPMFlow;
using BPMFlow.Domain.Dtos.Entities.PerfManagement1;
using BPMFlow.Domain.Dtos.Filters;
using BPMFlow.Domain.Interfaces.Repositories;
using BPMFlow.Domain.Models.Entities.BPMFlow;
using BPMFlow.Domain.Models.Enums;
using BPMFlow.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BPMFlow.Infrastructure.Data.Repositories;

public class ObjectRequestRepository : IObjectRequestRepository
{
    private readonly BPMFlowDbContext _bpmFlowContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ObjectRequestRepository(BPMFlowDbContext bpmFlowContext, IUnitOfWork unitOfWork,IMapper mapper)
    {
        _bpmFlowContext = bpmFlowContext;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ObjectRequestDto> GetById(int requestId)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(requestId);

        return await _bpmFlowContext.ObjectRequests
            .AsNoTracking()
            .ProjectTo<ObjectRequestDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == requestId);
    }

    public async Task<IEnumerable<ObjectRequestDto>> GetBySystemObjectId()
    {
        return await _bpmFlowContext.ObjectRequests
                        .AsNoTracking()
                        .ProjectTo<ObjectRequestDto>(_mapper.ConfigurationProvider)
                        .Join(_bpmFlowContext.RequestStatuses,
                                or => or.RequestStatusId,
                                rs => rs.Id,
                                (or, rs) => new { or, rs })
                        .Join(_bpmFlowContext.Requests,
                                ors => ors.rs.RequestId,
                                r => r.Id,
                                (ors, r) => new { ors.or, ors.rs, r })
                        .Join(_bpmFlowContext.BusinessProcesses,
                                orsr => orsr.r.BusinessProcessId,
                                bp => bp.Id,
                                (orsr, bp) => new { orsr.or, orsr.rs, orsr.r, bp })
                        .Where(result => result.bp.SystemId == (int)SystemObjects.Employee)
                        .Select(result => result.or)
                        .ToListAsync();
    }

    public async Task<ObjectRequestDto> Create(ObjectRequestDto objectRequestDto)
    {
        ArgumentNullException.ThrowIfNull(objectRequestDto);

        var maxCode = await _bpmFlowContext.ObjectRequests.AnyAsync()
            ? await _bpmFlowContext.ObjectRequests.MaxAsync(x => x.Code)
            : 0;

        var request = new ObjectRequest
        {
            Code = ++maxCode,
            RequestStatusId = objectRequestDto.RequestStatusId,
            ObjectId = objectRequestDto.ObjectId,
            PeriodId = objectRequestDto.PeriodId,
            DateStart = DateTime.Now,
            DateEnd = DateTime.MaxValue,
            IsActive = true,
            EntityStatusId = (int)EntityStatuses.ActiveDraft
        };

        _bpmFlowContext.ObjectRequests.Add(request);

        await _bpmFlowContext.SaveChangesAsync();

        return await GetById(request.Id);
    }

    public async Task<IEnumerable<ObjectRequestDto>> GetByFilter(ObjectRequestsFilterDto filterDto)
    {
        ArgumentNullException.ThrowIfNull(filterDto);

        var query = _bpmFlowContext.ObjectRequests
            .AsNoTracking()
            .ProjectTo<ObjectRequestDto>(_mapper.ConfigurationProvider);

        if (filterDto.ObjectId.HasValue && filterDto.ObjectId.Value != 0)
        {
            query = query.Where(x => x.ObjectId == filterDto.ObjectId.Value);
        }

        if (filterDto.SystemId.HasValue && filterDto.SystemId.Value != 0)
        {
            query = query.Join(_bpmFlowContext.RequestStatuses,
                                or => or.RequestStatusId,
                                rs => rs.Id,
                                (or, rs) => new { or, rs })
                        .Join(_bpmFlowContext.Requests,
                                ors => ors.rs.RequestId,
                                r => r.Id,
                                (ors, r) => new { ors.or, ors.rs, r })
                        .Join(_bpmFlowContext.BusinessProcesses,
                                orsr => orsr.r.BusinessProcessId,
                                bp => bp.Id,
                                (orsr, bp) => new { orsr.or, orsr.rs, orsr.r, bp })
                        .Where(result => result.bp.SystemId == filterDto.SystemId)
                        .Select(result => result.or);
        }

        if (filterDto.SystemObjectId.HasValue && filterDto.SystemObjectId.Value != 0)
        {
            query = query.Join(_bpmFlowContext.RequestStatuses,
                                or => or.RequestStatusId,
                                rs => rs.Id,
                                (or, rs) => new { or, rs })
                        .Join(_bpmFlowContext.Requests,
                                ors => ors.rs.RequestId,
                                r => r.Id,
                                (ors, r) => new { ors.or, ors.rs, r })
                        .Join(_bpmFlowContext.BusinessProcesses,
                                orsr => orsr.r.BusinessProcessId,
                                bp => bp.Id,
                                (orsr, bp) => new { orsr.or, orsr.rs, orsr.r, bp })
                        .Where(result => result.bp.SystemObjectId == filterDto.SystemObjectId)
                        .Select(result => result.or);
        }

        if (filterDto.RequestStatusId.HasValue && filterDto.RequestStatusId.Value != 0)
        {
            query = query.Where(x => x.RequestStatusId == filterDto.RequestStatusId.Value);
        }

        if (filterDto.PeriodIds is not null && filterDto.PeriodIds.Count != 0)
        {
            query = query.Where(x => filterDto.PeriodIds.Contains(x.PeriodId));
        }

        if (filterDto.SubordinateEmployeeIds != null && filterDto.SubordinateEmployeeIds.Count != 0)
        {
            query = query.Where(x => filterDto.SubordinateEmployeeIds.Contains(x.ObjectId) || x.ResponsibleEmployeeId == filterDto.ObjectId);
        }
        else if (filterDto.ObjectId.HasValue && filterDto.ObjectId.Value != 0)
        {
            query = query.Where(x => x.ObjectId == filterDto.ObjectId.Value);
        }

        return await query.ToListAsync();
    }

    public async Task<ObjectRequestDto> ChangeStatus(ObjectRequestDto objectRequestDto, int nextStatusOrder)
    {
        ArgumentNullException.ThrowIfNull(objectRequestDto);

        var objectRequest = await _bpmFlowContext.ObjectRequests
            .Include(or => or.RequestStatus)
            .FirstOrDefaultAsync(or => or.Id == objectRequestDto.ObjectId);

        ArgumentNullException.ThrowIfNull(objectRequest);
        
        // сценарии перехода
        var transition = await _bpmFlowContext.RequestStatusTransitions
            .FirstOrDefaultAsync(x => x.SourceStatusOrder == objectRequest.RequestStatus.StatusOrder &&
                                    x.NextStatusOrder == nextStatusOrder &&
                                    x.RequestId == objectRequest.RequestStatus.RequestId);

        // закрываем текущую запись
        CloseCurrentRequest(objectRequest, transition.Id);

        // наличие параллельных этапов
        var parallelRequests = await _bpmFlowContext.ObjectRequests
            .Where(or => or.Code == objectRequest.Code && or.EntityStatusId == 1)
            .ToListAsync();

        if (parallelRequests.Count == 0)
        {
            await TransitionToNextStatus(objectRequest, nextStatusOrder);
        }
        else
        {
            if (transition.SourceStatusOrder > transition.NextStatusOrder)
            {
                await HandleBackwardTransition(objectRequest, parallelRequests, nextStatusOrder);
            }
            else if (transition.SourceStatusOrder < transition.NextStatusOrder)
            {
                await HandleForwardTransition(objectRequest, parallelRequests, nextStatusOrder);
            }
        }

        await _bpmFlowContext.SaveChangesAsync();

        return _mapper.Map<ObjectRequestDto>(objectRequest);
    }

    private static void CloseCurrentRequest(ObjectRequest objectRequest, int transitionId)
    {
        objectRequest.RequestStatusTransitionId = transitionId;
        objectRequest.EntityStatusId = 2;
        objectRequest.DateEnd = DateTime.Now;
    }

    private async Task TransitionToNextStatus(ObjectRequest objectRequest, int nextStatusOrder)
    {
        var nextStatuses = await _bpmFlowContext.RequestStatuses
            .Where(rs => rs.StatusOrder == nextStatusOrder && rs.RequestId == objectRequest.RequestStatus.RequestId)
            .ToListAsync();

        foreach (var nextStatus in nextStatuses)
        {
            var newObjectRequest = new ObjectRequest
            {
                ObjectId = objectRequest.ObjectId,
                AuthorEmployeeId = objectRequest.AuthorEmployeeId,
                Code = objectRequest.Code,
                RequestStatusId = nextStatus.Id,
                ResponsibleEmployeeId = await _unitOfWork.EmployeeRepository.GetResponsibleEmployeeId(nextStatus.ResponsibleRoleId),
                DateStart = DateTime.Now,
                DateEnd = DateTime.MaxValue,
                EntityStatusId = 1
            };

            _bpmFlowContext.ObjectRequests.Add(newObjectRequest);
        }
    }

    private async Task HandleBackwardTransition(ObjectRequest objectRequest, List<ObjectRequest> parallelRequests, int nextStatusOrder)
    {
        foreach (var parallelRequest in parallelRequests)
        {
            parallelRequest.EntityStatusId = 2;
            parallelRequest.DateEnd = DateTime.Now;
        }

        await TransitionToNextStatus(objectRequest, nextStatusOrder);
    }

    private async Task HandleForwardTransition(ObjectRequest objectRequest, List<ObjectRequest> parallelRequests, int nextStatusOrder)
    {
        var nextStatuses = await _bpmFlowContext.RequestStatuses
            .Where(rs => rs.StatusOrder == nextStatusOrder && rs.RequestId == objectRequest.RequestStatus.RequestId)
            .ToListAsync();

        foreach (var nextStatus in nextStatuses)
        {
            if (nextStatus.IsFinalDenied)
            {
                foreach (var parallelRequest in parallelRequests)
                {
                    parallelRequest.EntityStatusId = 2;
                    parallelRequest.DateEnd = DateTime.Now;
                }

                var newObjectRequest = new ObjectRequest
                {
                    ObjectId = objectRequest.ObjectId,
                    AuthorEmployeeId = objectRequest.AuthorEmployeeId,
                    Code = objectRequest.Code,
                    RequestStatusId = nextStatus.Id,
                    ResponsibleEmployeeId = await _unitOfWork.EmployeeRepository.GetResponsibleEmployeeId(nextStatus.ResponsibleRoleId),
                    DateStart = DateTime.Now,
                    DateEnd = DateTime.MaxValue,
                    EntityStatusId = 3
                };

                _bpmFlowContext.ObjectRequests.Add(newObjectRequest);
            }
        }
    }
}