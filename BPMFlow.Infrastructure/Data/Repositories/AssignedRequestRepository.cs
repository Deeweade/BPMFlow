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

public class AssignedRequestRepository : IAssignedRequestRepository
{
    private readonly BPMFlowDbContext _bpmFlowContext;
    private readonly PerfManagement1DbContext _perfManagement1Context;
    private readonly IMapper _mapper;

    public AssignedRequestRepository(BPMFlowDbContext bpmFlowContext, PerfManagement1DbContext perfManagement1Context, IMapper mapper)
    {
        _bpmFlowContext = bpmFlowContext;
        _perfManagement1Context = perfManagement1Context;
        _mapper = mapper;
    }

    public async Task<AssignedRequestDto> GetById(int requestId)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(requestId);

        return await _bpmFlowContext.AssignedRequests
            .AsNoTracking()
            .ProjectTo<AssignedRequestDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == requestId);
    }

    public async Task<AssignedRequestDto> Create(AssignedRequestDto assignedRequestDto)
    {
        ArgumentNullException.ThrowIfNull(assignedRequestDto);

        var employee = await _perfManagement1Context.Employees
            .AsNoTracking()
            .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == assignedRequestDto.EmployeeId);

        ArgumentNullException.ThrowIfNull(employee);

        var maxCode = await _bpmFlowContext.AssignedRequests.AnyAsync()
            ? await _bpmFlowContext.AssignedRequests.MaxAsync(x => x.Code)
            : 0;

        var request = new AssignedRequest
        {
            Code = ++maxCode,
            GroupRequestId = assignedRequestDto.GroupRequestId,
            RequestStatusId = assignedRequestDto.RequestStatusId,
            ResponsibleEmployeeId = (int)employee.Parent,
            EmployeeId = assignedRequestDto.EmployeeId,
            PeriodId = assignedRequestDto.PeriodId,
            DateStart = DateTime.Now,
            DateEnd = DateTime.MaxValue,
            IsActive = true,
            EntityStatusId = (int)EntityStatuses.ActiveDraft
        };

        _bpmFlowContext.AssignedRequests.Add(request);

        await _bpmFlowContext.SaveChangesAsync();

        return await GetById(request.Id);
    }

    public async Task<IEnumerable<AssignedRequestDto>> GetByFilter(AssignedRequestsFilterDto filterDto)
    {
        ArgumentNullException.ThrowIfNull(filterDto);

        var query = _bpmFlowContext.AssignedRequests
            .AsNoTracking()
            .ProjectTo<AssignedRequestDto>(_mapper.ConfigurationProvider);

        if (filterDto.GroupRequestId.HasValue)
        {
            query = query.Where(x => x.GroupRequestId == filterDto.GroupRequestId.Value);
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
            query = query.Where(x => filterDto.SubordinateEmployeeIds.Contains(x.EmployeeId) || x.ResponsibleEmployeeId == filterDto.EmployeeId);
        }
        else if (filterDto.EmployeeId.HasValue && filterDto.RequestStatusId.Value != 0)
        {
            query = query.Where(x => x.EmployeeId == filterDto.EmployeeId.Value);
        }

        var result = await query.ToListAsync();

        return result;
    }
}