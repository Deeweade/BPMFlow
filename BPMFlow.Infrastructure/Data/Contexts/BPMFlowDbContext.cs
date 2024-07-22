using BPMFlow.Domain.Models.Entities.BPMFlow;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BPMFlow.Infrastructure.Data.Contexts;

public class BPMFlowDbContext : DbContext
{
    public BPMFlowDbContext(DbContextOptions<BPMFlowDbContext> options)
        : base (options)
    {
    }

    public DbSet<ObjectRequest> ObjectRequests { get; set; }
    public DbSet<Request> Requests {get; set; }
    public DbSet<RequestStatusTransition> RequestStatusTransitions { get; set; }
    public DbSet<RequestStatus> RequestStatuses { get; set; }
    public DbSet<BusinessProcess> BusinessProcesses { get; set; }
    public DbSet<RequestStatusTrigger> RequestStatusTriggers { get; set; }
    public DbSet<Domain.Models.Entities.BPMFlow.System> Systems { get; set; }
    public DbSet<SystemObject> SystemObjects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ObjectRequest>()
            .HasOne(ar => ar.RequestStatus)
            .WithMany(rs => rs.ObjectRequests)
            .HasForeignKey(ar => ar.RequestStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RequestStatus>()
            .HasOne(rs => rs.Request)
            .WithMany(gr => gr.RequestStatuses)
            .HasForeignKey(rs => rs.RequestId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Request>()
            .HasMany(rs => rs.RequestStatuses)
            .WithOne(gr => gr.Request)
            .HasForeignKey(rs => rs.RequestId)
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(modelBuilder);
    }

}