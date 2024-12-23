using Microsoft.EntityFrameworkCore;
using MyContact_API.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

public class MyContactDbContext : DbContext
{
    public DbSet<Sites> Sites { get; set; }
    public DbSet<Services> Services { get; set; }
    public DbSet<Salaries> Salaries { get; set; }

    public MyContactDbContext(DbContextOptions<MyContactDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Salaries>()
            .HasOne(s => s.Service)
            .WithMany(s => s.Salaries)
            .HasForeignKey(s => s.ServiceId);

        modelBuilder.Entity<Salaries>()
            .HasOne(s => s.Site)
            .WithMany(s => s.Salaries)
            .HasForeignKey(s => s.SiteId);
    }
}
