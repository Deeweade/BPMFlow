using AutoMapper;
using AutoMapper.QueryableExtensions;
using BPMFlow.Domain.Dtos.Entities.PerfManagement1;
using BPMFlow.Domain.Interfaces.Repositories;
using BPMFlow.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BPMFlow.Infrastructure.Data.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly PerfManagement1DbContext _perfManagement1DbContext;
    private readonly IMapper _mapper;
    public EmployeeRepository(PerfManagement1DbContext perfManagement1DbContext, IMapper mapper)
    {
        _perfManagement1DbContext = perfManagement1DbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<int>> GetSubordinateEmployeeIds(int employeeId)
    {
        return await _perfManagement1DbContext.Employees
                             .AsNoTracking()
                             .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
                             .Where(e => e.Id == employeeId)
                             .Select(e => e.Id)
                             .ToListAsync();
    }
}