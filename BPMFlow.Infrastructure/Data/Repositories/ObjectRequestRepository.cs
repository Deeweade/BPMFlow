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

    /* public async Task<ObjectRequestDto> Create(ObjectRequestDto objectRequestDto)
    {
        ArgumentNullException.ThrowIfNull(objectRequestDto);

        var employee = await _perfManagement1Context.Employees
            .AsNoTracking()
            .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == objectRequestDto.EmployeeId);

        ArgumentNullException.ThrowIfNull(employee);

        var maxCode = await _bpmFlowContext.ObjectRequests.AnyAsync()
            ? await _bpmFlowContext.ObjectRequests.MaxAsync(x => x.Code)
            : 0;

        var request = new ObjectRequest
        {
            Code = ++maxCode,
            RequestStatusId = objectRequestDto.RequestStatusId,
            ResponsibleEmployeeId = (int)employee.Parent,
            EmployeeId = objectRequestDto.EmployeeId,
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

        var query = await _bpmFlowContext.ObjectRequests
            .AsNoTracking()
            .ProjectTo<ObjectRequestDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        if (filterDto.RequestStatusId.HasValue && filterDto.RequestStatusId.Value != 0)
        {
            query = query.Where(x => x.RequestStatusId == filterDto.RequestStatusId.Value).ToList();
        }

        if (filterDto.PeriodIds is not null && filterDto.PeriodIds.Count != 0)
        {
            query = query.Where(x => filterDto.PeriodIds.Contains(x.PeriodId)).ToList();
        }

        if (filterDto.SubordinateEmployeeIds != null && filterDto.SubordinateEmployeeIds.Count != 0)
        {
            query = query.Where(x => filterDto.SubordinateEmployeeIds.Contains(x.EmployeeId) || x.ResponsibleEmployeeId == filterDto.EmployeeId).ToList();
        }
        else if (filterDto.EmployeeId.HasValue && filterDto.EmployeeId.Value != 0)
        {
            query = query.Where(x => x.EmployeeId == filterDto.EmployeeId.Value).ToList();
        }

        return query;
    } */

    public async Task<ObjectRequestDto> ChangeStatus(ObjectRequestDto objectRequestDto, int nextStatusOrder)
    {
        
    }
}