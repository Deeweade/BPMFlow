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

        modelBuilder.Entity<BusinessProcess>()
            .HasOne(bp => bp.SystemObject)
            .WithMany(so => so.BusinessProcesses)
            .HasForeignKey(bp => bp.SystemObjectId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<BusinessProcess>()
            .HasOne(bp => bp.System)
            .WithMany(s => s.BusinessProcesses)
            .HasForeignKey(bp => bp.SystemId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<SystemObject>()
            .HasOne(so => so.System)
            .WithMany(s => s.SystemObjects)
            .HasForeignKey(so => so.SystemId)
            .OnDelete(DeleteBehavior.Restrict);
            
        modelBuilder.Entity<ObjectRequest>()
            .HasOne(or => or.RequestStatus)
            .WithMany(rs => rs.ObjectRequests)
            .HasForeignKey(or => or.RequestStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ObjectRequest>()
            .HasOne(or => or.Request)
            .WithMany(r => r.ObjectRequests)
            .HasForeignKey(or => or.RequestId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ObjectRequest>()
            .HasOne(or => or.RequestStatusTransition)
            .WithMany(rst => rst.ObjectRequests)
            .HasForeignKey(or => or.RequestStatusTransitionId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RequestStatusTransition>()
            .HasOne(rst => rst.Request)
            .WithMany(r => r.RequestStatusTransitions)
            .HasForeignKey(rst => rst.RequestId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Request>()
            .HasOne(r => r.BusinessProcess)
            .WithMany(bp => bp.Requests)
            .HasForeignKey(r => r.BusinessProcessId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RequestStatus>()
            .HasOne(rs => rs.Request)
            .WithMany(r => r.RequestStatuses)
            .HasForeignKey(rs => rs.RequestId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RequestStatus>()
            .HasMany(rs => rs.RequestStatusTriggers)
            .WithOne(rst => rst.RequestStatus)
            .HasForeignKey(rs => rs.RequestStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(modelBuilder);
    }

}