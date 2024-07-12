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
        if (requestId <= 0 ) throw new ArgumentOutOfRangeException(nameof(requestId));

        return await _bpmFlowContext.AssignedRequests
            .AsNoTracking()
            .ProjectTo<AssignedRequestDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == requestId);
    }

    public async Task<AssignedRequestDto> Create(AssignedRequestDto assignedRequestDto)
    {
        if (assignedRequestDto is null) throw new ArgumentNullException(nameof(assignedRequestDto));
        
        var employee = await _perfManagement1Context.Employees
                       .AsNoTracking()
                       .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
                       .FirstOrDefaultAsync(x => x.Id == assignedRequestDto.EmployeeId);

        if (employee is null) throw new ArgumentNullException(nameof(employee));

        var maxCode = await _bpmFlowContext.AssignedRequests.MaxAsync(x => x.Code);

        var request = new AssignedRequest
        {
            Code = ++maxCode,
            GroupRequestId = assignedRequestDto.GroupRequestId,
            RequestStatusId = assignedRequestDto.RequestStatusId,
            ResponsibleEmployeeId = employee.Parent,
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
        if (filterDto is null) throw new ArgumentNullException(nameof(filterDto));

        var query = _bpmFlowContext.AssignedRequests
            .AsNoTracking()
            .ProjectTo<AssignedRequestDto>(_mapper.ConfigurationProvider);

        if (filterDto.GroupRequestId > 0)
        {
            query = query.Where(x => x.GroupRequestId == filterDto.GroupRequestId);
        }

        if (filterDto.RequestStatusId > 0)
        {
            query = query.Where(x => x.RequestStatusId == filterDto.RequestStatusId);
        }

        if (filterDto.PeriodIds is not null && filterDto.PeriodIds.Any())
        {
            query = query.Where(x => filterDto.PeriodIds.Contains(x.PeriodId));
        }

        if (filterDto.SubordinateEmployeeIds != null && filterDto.SubordinateEmployeeIds.Any())
        {
            query = query.Where(x => filterDto.SubordinateEmployeeIds.Contains(x.EmployeeId) || x.ResponsibleEmployeeId == filterDto.EmployeeId);
        }
        else if (filterDto.EmployeeId > 0)
        {
            query = query.Where(x => x.EmployeeId == filterDto.EmployeeId);
        }

        var result = await query.ToListAsync();

        return result;
    }
}