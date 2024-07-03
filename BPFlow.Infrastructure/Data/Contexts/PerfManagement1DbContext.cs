using BPFlow.Domain.Models.Entities.PerfManagement1;
using Microsoft.EntityFrameworkCore;


namespace BPFlow.Infrastructure.Data.Contexts;

public class PerfManagement1DbContext : DbContext
{
    public PerfManagement1DbContext(DbContextOptions<BPFlowDbContext> options)
        : base (options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Role> Roles {get; set; }
    public DbSet<Domain.Models.Entities.PerfManagement1.Action> Actions { get; set; }
    public DbSet<EmployeeRole> EmployeeRoles { get; set; }
    public DbSet<RoleType> RoleTypes { get; set; }
    public DbSet<RoleAllowedAction> RoleAllowedActions { get; set; }
    public DbSet<EntityStatus> EntityStatuses { get; set; }
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

}