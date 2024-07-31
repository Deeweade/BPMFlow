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

    public async Task<EmployeeDto> GetById(int employeeId)
    {
        return await _perfManagement1DbContext.Employees
                            .AsNoTracking()
                            .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
                            .Where(x => x.Id == employeeId)
                            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<int>> GetSubordinateEmployeeIds(int employeeId)
    {
        return await _perfManagement1DbContext.Employees
                             .AsNoTracking()
                             .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
                             .Where(e => e.Parent == employeeId)
                             .Select(e => e.Id)
                             .ToListAsync();
    }

    public async Task<int> GetResponsibleEmployeeId(int responsibleRoleId)
    {
        return (int)await _perfManagement1DbContext.EmployeeRoles
                            .AsNoTracking()
                            .ProjectTo<EmployeeRoleDto>(_mapper.ConfigurationProvider)
                            .Where(x => x.RoleId == responsibleRoleId)
                            .Select(x => x.EmployeeId)
                            .FirstOrDefaultAsync();
    }

    public async Task<EmployeeDto> GetByUserLogin(string login)
    {
        return await _perfManagement1DbContext.Employees
                            .AsNoTracking()
                            .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
                            .Where(x => x.Login == login)
                            .FirstOrDefaultAsync();
    }
}