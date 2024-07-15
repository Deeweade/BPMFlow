using BPMFlow.Domain.Dtos.Entities.PerfManagement1;

namespace BPMFlow.Domain.Interfaces.Repositories;

public interface IPeriodRepository
{
    Task<PeriodDto> GetById(int periodId);
    Task<IEnumerable<int>> GetChildPeriodIds(int parentPeriodId);
}