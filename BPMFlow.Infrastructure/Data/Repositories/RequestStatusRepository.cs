using AutoMapper;
using AutoMapper.QueryableExtensions;
using BPMFlow.Domain.Dtos.Entities.BPMFlow;
using BPMFlow.Domain.Interfaces.Repositories;
using BPMFlow.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BPMFlow.Infrastructure.Data.Repositories;

public class RequestStatusRepository : IRequestStatusRepository
{
    private readonly BPMFlowDbContext _bpmFlowDbContext;
    private readonly IMapper _mapper;

    public RequestStatusRepository(BPMFlowDbContext bpmFlowDbContext, IMapper mapper)
    {
        _bpmFlowDbContext = bpmFlowDbContext;
        _mapper = mapper;
    }

    public async Task<RequestStatusDto> GetById(int requestStatusId)
    {
        return await _bpmFlowDbContext.RequestStatuses
                .AsNoTracking()
                .ProjectTo<RequestStatusDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == requestStatusId);
    }

    public async Task<IEnumerable<RequestStatusDto>> GetByOrderAndRequestId(int order, int requestId)
    {
        return await _bpmFlowDbContext.RequestStatuses
                .AsNoTracking()
                .ProjectTo<RequestStatusDto>(_mapper.ConfigurationProvider)
                .Where(x => x.StatusOrder == order && x.RequestId == requestId)
                .ToListAsync();
    }

    public async Task<IEnumerable<RequestStatusDto>> GetByRequestId(int requestId)
    {
        return await _bpmFlowDbContext.RequestStatuses
                .AsNoTracking()
                .ProjectTo<RequestStatusDto>(_mapper.ConfigurationProvider)
                .Where(x => x.RequestId == requestId)
                .ToListAsync();
    }

    public async Task<int> GetResponsibleRoleIdByStatusId(int requestStatusId)
    {
        var requestStatus = await _bpmFlowDbContext.RequestStatuses
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == requestStatusId);

        return requestStatus.ResponsibleRoleId;
    }
}