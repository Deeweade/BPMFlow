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
    private readonly PerfManagement1DbContext _perfManagement1Context;
    private readonly IMapper _mapper;

    public ObjectRequestRepository(BPMFlowDbContext bpmFlowContext, PerfManagement1DbContext perfManagement1Context, IMapper mapper)
    {
        _bpmFlowContext = bpmFlowContext;
        _perfManagement1Context = perfManagement1Context;
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
            SystemId = objectRequestDto.SystemId,
            SystemObjectId = objectRequestDto.SystemObjectId,
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

        if (filterDto.SystemId.HasValue && filterDto.SystemId.Value != 0)
        {
            query = query.Where(x => x.SystemId == filterDto.SystemId);
            /* query = query.Join(_bpmFlowContext.RequestStatuses,
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
                        .Select(result => result.or); */
        }

        if (filterDto.SystemObjectId.HasValue && filterDto.SystemObjectId.Value != 0)
        {
            query = query.Where(x => x.SystemObjectId == filterDto.SystemObjectId);
            /* query = query.Join(_bpmFlowContext.RequestStatuses,
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
                        .Select(result => result.or); */
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
            var employees = query.Where(x => x.SystemObjectId == (int)SystemObjects.Employee);
            
            if (employees.Any())
            {
                query = query.Where(x => filterDto.SubordinateEmployeeIds.Contains(x.ObjectId) || x.ResponsibleEmployeeId == filterDto.ObjectId);
            }
        }
        else if (filterDto.ObjectId.HasValue && filterDto.ObjectId.Value != 0)
        {
            query = query.Where(x => x.ObjectId == filterDto.ObjectId.Value);
        }

        return await query.ToListAsync();
    }
}