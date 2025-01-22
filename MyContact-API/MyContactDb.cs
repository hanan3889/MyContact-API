using Microsoft.EntityFrameworkCore;
using MyContact_API.Models;


public class MyContactDbContext : DbContext
{
    public DbSet<Sites> Sites { get; set; }
    public DbSet<Services> Services { get; set; }
    public DbSet<Salaries> Salaries { get; set; }

    public MyContactDbContext(DbContextOptions<MyContactDbContext> options) : base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Salaries>()
            .HasOne(s => s.Service)
            .WithMany()
            .HasForeignKey(s => s.ServiceId);

        modelBuilder.Entity<Salaries>()
            .HasOne(s => s.Site)
            .WithMany()
            .HasForeignKey(s => s.SiteId);
    }
}
