using AutoMapper;
using BPMFlow.Domain.Interfaces.Repositories;
using BPMFlow.Infrastructure.Data.Contexts;
using BPMFlow.Infrastructure.Data.Repositories;

namespace BPMFlow.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly BPMFlowDbContext _bpmFlowDbContext;
    private readonly PerfManagement1DbContext _perfManagement1DbContext;

    public UnitOfWork(BPMFlowDbContext bpmFlowDbContext, PerfManagement1DbContext perfManagement1DbContext, IMapper mapper)
    {
        _bpmFlowDbContext = bpmFlowDbContext;
        _perfManagement1DbContext = perfManagement1DbContext;

        ObjectRequestRepository = new ObjectRequestRepository(_bpmFlowDbContext, mapper);
        PeriodRepository = new PeriodRepository(_perfManagement1DbContext, mapper);
        EmployeeRepository = new EmployeeRepository(_perfManagement1DbContext, mapper);
        RequestRepository = new RequestRepository(_bpmFlowDbContext, mapper);
        RequestStatusRepository = new RequestStatusRepository(_bpmFlowDbContext, mapper);
        RequestStatusTransitionRepository = new RequestStatusTransitionRepository(_bpmFlowDbContext, mapper);
    }

    public IObjectRequestRepository ObjectRequestRepository { get; }
    public IPeriodRepository PeriodRepository { get; }
    public IEmployeeRepository EmployeeRepository { get; }
    public IRequestRepository RequestRepository { get; }
    public IRequestStatusRepository RequestStatusRepository { get; }
    public IRequestStatusTransitionRepository RequestStatusTransitionRepository { get; }

    public async Task<int> SaveChangesAsync()
    {
        return await _bpmFlowDbContext.SaveChangesAsync();
    }
}
