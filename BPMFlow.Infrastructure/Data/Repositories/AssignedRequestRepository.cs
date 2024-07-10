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

public class AssignedRequestRepository : IAssignedRequestRepository
{
    private readonly BPMFlowDbContext _bpmFlowContext;
    private readonly PerfManagement1DbContext _perfManagement1DbContext;
    private readonly IMapper _mapper;

    public AssignedRequestRepository(BPMFlowDbContext bpmFlowContext, PerfManagement1DbContext perfManagement1DbContext, IMapper mapper)
    {
        _bpmFlowContext = bpmFlowContext;
        _perfManagement1DbContext = perfManagement1DbContext;
        _mapper = mapper;
    }

    public async Task<AssignedRequestDto?> GetById(int requestId)
    {
        if (requestId <= 0 ) throw new ArgumentOutOfRangeException(nameof(requestId));

        return await _bpmFlowContext.AssignedRequests
            .AsNoTracking()
            .ProjectTo<AssignedRequestDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == requestId);
    }

    public async Task<AssignedRequestDto?> Create(AssignedRequestDto assignedRequestDto)
    {
        if (assignedRequestDto is null) throw new ArgumentNullException(nameof(assignedRequestDto));

        var assignedRequest = _mapper.Map<AssignedRequest>(assignedRequestDto);
        
        var employee = await _perfManagement1DbContext.Employees.FindAsync(assignedRequestDto.EmployeeId);

        if (employee is null) throw new ArgumentNullException(nameof(employee));

        var maxCode = await _bpmFlowContext.AssignedRequests.MaxAsync(x => x.Code);

        var request = new AssignedRequest
        {
            Code = maxCode + 1,
            GroupRequestId = assignedRequestDto.GroupRequestId,
            RequestStatusId = assignedRequestDto.RequestStatusId,
            ResponsibleEmployeeId = employee.Parent,
            EmployeeId = assignedRequestDto.EmployeeId,
            PeriodId = assignedRequestDto.PeriodId,
            DateStart = DateTime.Now,
            DateEnd = DateTime.MaxValue,
            IsActive = true,
            EntityStatusId = (int?)EntityStatuses.ActiveDraft
        };

        _bpmFlowContext.Add(assignedRequest);

        await _bpmFlowContext.SaveChangesAsync();

        return await GetById(assignedRequest.Id);
    }

    public async Task<IEnumerable<AssignedRequestDto>> GetByFilter(AssignedRequestsFilterDto filterDto)
    {
    
        var query = _bpmFlowContext.AssignedRequests
            .AsNoTracking()
            .ProjectTo<AssignedRequestDto>(_mapper.ConfigurationProvider);

        if (filterDto.EmployeeId is not null)
        {
            query = query.Where(x => x.EmployeeId.HasValue && x.EmployeeId == filterDto.EmployeeId.Value);
        }

        if (filterDto.PeriodId is not null)
        {
            query = query.Where(x => x.PeriodId.HasValue && x.PeriodId == filterDto.PeriodId.Value);
        }

        if (filterDto.GroupRequestId is not null)
        {
            query = query.Where(x => x.GroupRequestId.HasValue && x.GroupRequestId == filterDto.GroupRequestId.Value);
        }

        if (filterDto.RequestStatusId is not null)
        {
            query = query.Where(x => x.RequestStatusId.HasValue && x.RequestStatusId == filterDto.RequestStatusId.Value);
        }

        var result = await query.ToListAsync();

        return result;
    }
}