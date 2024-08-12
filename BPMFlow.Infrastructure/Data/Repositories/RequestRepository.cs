using AutoMapper;
using AutoMapper.QueryableExtensions;
using BPMFlow.Domain.Dtos.Entities.BPMFlow;
using BPMFlow.Domain.Dtos.Filters;
using BPMFlow.Domain.Interfaces.Repositories;
using BPMFlow.Domain.Models.Enums;
using BPMFlow.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BPMFlow.Infrastructure.Data.Repositories;

public class RequestRepository(BPMFlowDbContext bpmFlowDbContext, IMapper mapper) : IRequestRepository
{
    private readonly BPMFlowDbContext _bpmFlowDbContext = bpmFlowDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<RequestDto>> GetByFilter(RequestFilterDto filterDto)
    {
        ArgumentNullException.ThrowIfNull(filterDto);

        var query = _bpmFlowDbContext.Requests
            .AsNoTracking();

        if (filterDto.EmployeeId.HasValue)
        {
            query = query.Where(r => r.ObjectRequests.Any(or => 
                or.ObjectId == filterDto.EmployeeId || 
                or.ResponsibleEmployeeId == filterDto.EmployeeId));
        }

        if (filterDto.PeriodId.HasValue)
        {
            query = query.Where(r => r.ObjectRequests.Any(or => or.PeriodId == filterDto.PeriodId));
        }

        if (filterDto.SubordinateEmployeeIds.Count != 0)
        {
            query = query.Where(r => r.ObjectRequests.Any(or => filterDto.SubordinateEmployeeIds.Contains(or.ObjectId)
                                                        || filterDto.SubordinateEmployeeIds.Contains(or.ResponsibleEmployeeId)));
        }

        query = query.Where(r => r.ObjectRequests.Any(or => or.IsActive && 
            (or.EntityStatusId == (int)EntityStatuses.ActiveDraft || or.EntityStatusId == (int)EntityStatuses.CompletedAndApproved)));

        return _mapper.Map<IEnumerable<RequestDto>>(await query.ToListAsync());
    }
}