using AutoMapper;
using AutoMapper.QueryableExtensions;
using BPMFlow.Domain.Dtos.Entities.BPMFlow;
using BPMFlow.Domain.Dtos.Entities.PerfManagement1;
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

        var assignedRequest = _mapper.Map<AssignedRequest>(assignedRequestDto);
        
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

        return await GetById(assignedRequest.Id);
    }
}