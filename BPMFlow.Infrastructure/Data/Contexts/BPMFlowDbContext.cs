using BPMFlow.Domain.Models.Entities.BPMFlow;
using Microsoft.EntityFrameworkCore;


namespace BPMFlow.Infrastructure.Data.Contexts;

public class BPMFlowDbContext : DbContext
{
    public BPMFlowDbContext(DbContextOptions<BPMFlowDbContext> options)
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
        modelBuilder.Entity<AssignedRequest>()
            .HasOne(ar => ar.RequestStatus)
            .WithMany(rs => rs.AssignedRequests)
            .HasForeignKey(ar => ar.RequestStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RequestStatus>()
            .HasOne(rs => rs.GroupRequest)
            .WithMany(gr => gr.RequestStatuses)
            .HasForeignKey(rs => rs.GroupRequestId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<GroupRequest>()
            .HasMany(rs => rs.AssignedRequests)
            .WithOne(gr => gr.GroupRequest)
            .HasForeignKey(rs => rs.GroupRequestId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<GroupRequest>()
            .HasMany(rs => rs.RequestStatuses)
            .WithOne(gr => gr.GroupRequest)
            .HasForeignKey(rs => rs.GroupRequestId)
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(modelBuilder);
    }

}