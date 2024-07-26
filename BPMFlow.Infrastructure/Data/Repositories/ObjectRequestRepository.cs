using AutoMapper;
using AutoMapper.QueryableExtensions;
using BPMFlow.Domain.Dtos.Entities.BPMFlow;
using BPMFlow.Domain.Dtos.Filters;
using BPMFlow.Domain.Interfaces.Repositories;
using BPMFlow.Domain.Models.Entities.BPMFlow;
using BPMFlow.Domain.Models.Entities.PerfManagement1;
using BPMFlow.Domain.Models.Enums;
using BPMFlow.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BPMFlow.Infrastructure.Data.Repositories;

public class ObjectRequestRepository : IObjectRequestRepository
{
    private readonly BPMFlowDbContext _bpmFlowContext;
    private readonly IMapper _mapper;

    public ObjectRequestRepository(BPMFlowDbContext bpmFlowContext, IMapper mapper)
    {
        _bpmFlowContext = bpmFlowContext;
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

    public async Task<IEnumerable<int>> GetBySystemObjectId()
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
                        .Select(result => result.or.Id)
                        .ToListAsync();
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
            var employees = await GetBySystemObjectId();

            foreach (var employee in employees)
            {
                query = query.Where(x => filterDto.SubordinateEmployeeIds.Contains(employee) || x.ResponsibleEmployeeId == filterDto.ObjectId);
            }

        }
        else if (filterDto.ObjectId.HasValue && filterDto.ObjectId.Value != 0)
        {
            query = query.Where(x => x.ObjectId == filterDto.ObjectId.Value);
        }

        return await query.ToListAsync();
    }
    
    public async Task<ObjectRequestDto> Create(ObjectRequestDto objectRequestDto)
    {
        ArgumentNullException.ThrowIfNull(objectRequestDto);

        var maxCode = await _bpmFlowContext.ObjectRequests.AnyAsync()
            ? await _bpmFlowContext.ObjectRequests.MaxAsync(x => x.Code)
            : 0;

        ArgumentNullException.ThrowIfNull(maxCode);

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

    public async Task CloseRequest(ObjectRequestDto objectRequestDto)
    {
        ArgumentNullException.ThrowIfNull(objectRequestDto);

        var entity = await _bpmFlowContext.ObjectRequests
                    .AsNoTracking()
                    .FirstOrDefaultAsync(or => or.Id == objectRequestDto.Id);

        ArgumentNullException.ThrowIfNull(entity);
            
        _mapper.Map(objectRequestDto, entity);
        _bpmFlowContext.ObjectRequests.Update(entity);
    
        await _bpmFlowContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<ObjectRequestDto>> GetParallelRequests(int code, int entityStatusId)
    {
        return await _bpmFlowContext.ObjectRequests
                .AsNoTracking()
                .ProjectTo<ObjectRequestDto>(_mapper.ConfigurationProvider)
                .Where(x => x.Code == code && x.EntityStatusId == entityStatusId)
                .ToListAsync();
    }

    public async Task AddObjectRequest(ObjectRequestDto objectRequestDto)
    {
        var entity = _mapper.Map<ObjectRequest>(objectRequestDto);
        
        await _bpmFlowContext.ObjectRequests.AddAsync(entity);
        await _bpmFlowContext.SaveChangesAsync();
    }
}