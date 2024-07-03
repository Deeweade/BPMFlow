using BPFlow.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;


namespace BPFlow.Infrastructure.Data.Contexts;

public class BPFlowDbContext : DbContext
{
    public BPFlowDbContext(DbContextOptions<BPFlowDbContext> options)
        : base (options)
    {
    }

    public DbSet<AssignedRequest> AssignedRequests { get; set; }
    public DbSet<GroupRequest> GroupRequests {get; set; }
    public DbSet<RequestStatusTransition> RequestStatusTransitions { get; set; }
    public DbSet<RequestStatusesOrder> RequestStatusesOrders { get; set; }
    public DbSet<RequestStatus> RequestStatuses { get; set; }
    public DbSet<BusinessProcess> BusinessProcess { get; set; }
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

}