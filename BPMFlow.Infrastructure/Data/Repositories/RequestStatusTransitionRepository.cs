using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BPMFlow.Domain.Dtos.Entities.BPMFlow;
using BPMFlow.Domain.Interfaces.Repositories;
using BPMFlow.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BPMFlow.Infrastructure.Data.Repositories;

public class RequestStatusTransitionRepository : IRequestStatusTransitionRepository
{
    private readonly BPMFlowDbContext _bpmFlowContext;
    private readonly IMapper _mapper;

    public RequestStatusTransitionRepository(BPMFlowDbContext bPMFlowDbContext, IMapper mapper)
    {
        _bpmFlowContext = bPMFlowDbContext;
        _mapper = mapper;
    }

    public async Task<RequestStatusTransitionDto> GetTransition(int sourceOrder, int nextOrder, int requestId)
    {
        return await _bpmFlowContext.RequestStatusTransitions
                .AsNoTracking()
                .ProjectTo<RequestStatusTransitionDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.SourceStatusOrder == sourceOrder && x.NextStatusOrder == nextOrder && x.RequestId == requestId);
    }
    
    public async Task<IEnumerable<RequestStatusTransitionDto>> GetAvailableTransition(Expression<Func<RequestStatusTransitionDto, bool>> predicate)
    {
        return await _bpmFlowContext.RequestStatusTransitions
            .AsNoTracking()
            .ProjectTo<RequestStatusTransitionDto>(_mapper.ConfigurationProvider)
            .Where(predicate)
            .ToListAsync();
    }
}