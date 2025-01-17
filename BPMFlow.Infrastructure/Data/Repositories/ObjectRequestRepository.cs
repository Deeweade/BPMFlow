using AutoMapper;
using AutoMapper.QueryableExtensions;
using BPMFlow.Domain.Dtos.Entities.BPMFlow;
using BPMFlow.Domain.Dtos.Filters;
using BPMFlow.Domain.Interfaces.Repositories;
using BPMFlow.Domain.Models.Entities.BPMFlow;
using BPMFlow.Domain.Models.Enums;
using BPMFlow.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BPMFlow.Infrastructure.Data.Repositories;

public class ObjectRequestRepository(BPMFlowDbContext bpmFlowDbContext, IMapper mapper) : IObjectRequestRepository
{
    private readonly BPMFlowDbContext _bpmFlowDbContext = bpmFlowDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<ObjectRequestDto> GetById(int requestId)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(requestId);

        return await _bpmFlowDbContext.ObjectRequests
            .AsNoTracking()
            .ProjectTo<ObjectRequestDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == requestId);
    }

    public async Task<ObjectRequestDto> GetActiveByCode(int code)
    {
        return await _bpmFlowDbContext.ObjectRequests
            .AsNoTracking()
            .ProjectTo<ObjectRequestDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Code == code 
                && x.IsActive 
                && (x.EntityStatusId == (int)EntityStatuses.ActiveDraft
                    || x.EntityStatusId == (int)EntityStatuses.CompletedAndApproved));
    }

    public async Task<IEnumerable<ObjectRequestDto>> GetManyActiveByCode(int[] codes)
    {
        return await _bpmFlowDbContext.ObjectRequests
            .AsNoTracking()
            .ProjectTo<ObjectRequestDto>(_mapper.ConfigurationProvider)
            .Where(x => codes.Contains(x.Code)
                && x.IsActive 
                && (x.EntityStatusId == (int)EntityStatuses.ActiveDraft
                    || x.EntityStatusId == (int)EntityStatuses.CompletedAndApproved))
            .ToListAsync();
    }

    public async Task<IEnumerable<ObjectRequestDto>> GetByResponsibleEmployeeId(int employeeId)
    {
        return await _bpmFlowDbContext.ObjectRequests
                .AsNoTracking()
                .ProjectTo<ObjectRequestDto>(_mapper.ConfigurationProvider)
                .Where(x => x.ResponsibleEmployeeId == employeeId)
                .ToListAsync();
    }

    public async Task<IEnumerable<ObjectRequestDto>> GetBySystemObjectIdEmployee()
    {
        var objectRequests = await _bpmFlowDbContext.ObjectRequests
                                .AsNoTracking()
                                .Where(x => x.RequestStatusTransition.Request.BusinessProcess.SystemObjectId == (int)SystemObjects.Employee)
                                .ToListAsync();

        return _mapper.Map<IEnumerable<ObjectRequestDto>>(objectRequests);
    }

    public async Task<IEnumerable<ObjectRequestDto>> GetByFilter(ObjectRequestsFilterDto filterDto)
    {
        ArgumentNullException.ThrowIfNull(filterDto);

        var query = _bpmFlowDbContext.ObjectRequests
            .AsNoTracking();

        if (filterDto.RequestId.HasValue && filterDto.RequestId.Value != 0)
        {
            query = query.Where(x => x.RequestStatus.RequestId == filterDto.RequestId.Value);
        }

        if (filterDto.SystemId.HasValue && filterDto.SystemId.Value != 0)
        {
            query = query.Where(x => x.RequestStatus.Request.BusinessProcess.SystemObject.SystemId == filterDto.SystemId);
        }

        if (filterDto.SystemObjectId.HasValue && filterDto.SystemObjectId.Value != 0)
        {
            query = query.Where(x => x.RequestStatus.Request.BusinessProcess.SystemObjectId == filterDto.SystemObjectId);
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
            var employees = await GetBySystemObjectIdEmployee();
            
            if (employees.Any())
            {
                query = query.Where(x => filterDto.SubordinateEmployeeIds.Contains(x.ObjectId) || x.ResponsibleEmployeeId == filterDto.ObjectId);
            }
        }
        else if (filterDto.ObjectId.HasValue && filterDto.ObjectId.Value != 0)
        {
            query.Where(x => x.ObjectId == filterDto.ObjectId.Value);
        }

        var result = await query.ToListAsync();

        return _mapper.Map<IEnumerable<ObjectRequestDto>>(result);
    }

    public async Task<ObjectRequestDto> Create(ObjectRequestDto objectRequestDto)
    {
        ArgumentNullException.ThrowIfNull(objectRequestDto);

        var maxCode = await _bpmFlowDbContext.ObjectRequests.AnyAsync()
            ? await _bpmFlowDbContext.ObjectRequests.MaxAsync(x => x.Code)
            : 0;

        var request = new ObjectRequest
        {
            Code = ++maxCode,
            RequestId = objectRequestDto.RequestId,
            RequestStatusId = (int)objectRequestDto.RequestStatusId,
            ObjectId = objectRequestDto.ObjectId,
            AuthorEmployeeId = objectRequestDto.AuthorEmployeeId,
            PeriodId = objectRequestDto.PeriodId,
            DateStart = DateTime.Now,
            DateEnd = DateTime.MaxValue,
            IsActive = true,
            EntityStatusId = (int)EntityStatuses.ActiveDraft
        };

        _bpmFlowDbContext.ObjectRequests.Add(request);

        await _bpmFlowDbContext.SaveChangesAsync();

        return await GetById(request.Id);
    }

    public async Task CloseRequest(ObjectRequestDto objectRequestDto)
    {
        ArgumentNullException.ThrowIfNull(objectRequestDto);

        var entity = await _bpmFlowDbContext.ObjectRequests
                    .AsNoTracking()
                    .FirstOrDefaultAsync(or => or.Id == objectRequestDto.Id);

        ArgumentNullException.ThrowIfNull(entity);
            
        _mapper.Map(objectRequestDto, entity);
        _bpmFlowDbContext.ObjectRequests.Update(entity);
    
        await _bpmFlowDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<ObjectRequestDto>> GetParallelRequests(int code, int entityStatusId)
    {
        return await _bpmFlowDbContext.ObjectRequests
                .AsNoTracking()
                .ProjectTo<ObjectRequestDto>(_mapper.ConfigurationProvider)
                .Where(x => x.Code == code && x.EntityStatusId == entityStatusId)
                .ToListAsync();
    }

    public async Task AddObjectRequest(ObjectRequestDto objectRequestDto)
    {
        var entity = _mapper.Map<ObjectRequest>(objectRequestDto);
        
        await _bpmFlowDbContext.ObjectRequests.AddAsync(entity);
        await _bpmFlowDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<ObjectRequestDto>> UpdateObjectRequests(List<ObjectRequestDto> activeRequests)
    {   
        foreach(var activeRequest in activeRequests)
        {
            _bpmFlowDbContext.ObjectRequests.Update(_mapper.Map<ObjectRequest>(activeRequest));
        }

        await _bpmFlowDbContext.SaveChangesAsync();

        return activeRequests;
    }
}