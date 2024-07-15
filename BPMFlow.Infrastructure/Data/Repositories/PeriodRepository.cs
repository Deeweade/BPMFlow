using AutoMapper;
using AutoMapper.QueryableExtensions;
using BPMFlow.Domain.Dtos.Entities.PerfManagement1;
using BPMFlow.Domain.Interfaces.Repositories;
using BPMFlow.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BPMFlow.Infrastructure.Data.Repositories;

public class PeriodRepository : IPeriodRepository
{
     private readonly PerfManagement1DbContext _perfManagement1DbContext;
    private readonly IMapper _mapper;
    
    public PeriodRepository(PerfManagement1DbContext perfManagement1DbContext, IMapper mapper)
    {
        _perfManagement1DbContext = perfManagement1DbContext;
        _mapper = mapper;
    }

    public async Task<PeriodDto> GetById(int periodId)
    {
        if (periodId <= 0) throw new ArgumentOutOfRangeException(nameof(periodId));

        return await _perfManagement1DbContext.Periods
            .AsNoTracking()
            .ProjectTo<PeriodDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == periodId);
    }
    public async Task<IEnumerable<int>> GetChildPeriodIds(int periodId)
    {
        if (periodId <= 0) throw new ArgumentOutOfRangeException(nameof(periodId));

        var parentPeriod = await _perfManagement1DbContext.Periods.FindAsync(periodId);

        return await _perfManagement1DbContext.Periods
            .AsNoTracking()
            .ProjectTo<PeriodDto>(_mapper.ConfigurationProvider)
            .Where(p => p.NumberY == parentPeriod.NumberY && p.IsYear == 0)
            .Select(p => p.Id)
            .ToListAsync();
    }
}