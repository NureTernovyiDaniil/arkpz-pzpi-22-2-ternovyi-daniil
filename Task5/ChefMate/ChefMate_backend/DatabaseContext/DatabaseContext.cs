using ChefMate_backend.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Menu> Menus { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<WorkZone> WorkZones { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Order>()
            .HasMany(oi=>oi.OrderItems)
            .WithOne(o=>o.Order)
            .HasForeignKey(fk=>fk.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Order>()
            .HasOne(o => o.Organization)
            .WithMany()
            .HasForeignKey(fk => fk.OrganizationId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<OrderItem>()
            .HasOne(oi => oi.MenuItem)
            .WithMany()
            .HasForeignKey(oi => oi.MenuItemId);

        builder.Entity<Menu>()
            .HasMany(m=>m.Items)
            .WithOne(mi => mi.Menu)
            .HasForeignKey(mi => mi.MenuId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Menu>()
            .HasOne(m => m.Organization)
            .WithMany(o => o.Menus)
            .HasForeignKey(fk => fk.OrganizationId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<MenuItem>()
            .HasOne<WorkZone>()
            .WithMany()
            .HasForeignKey(wz => wz.WorkZoneId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Organization>()
            .HasMany(wz => wz.WorkZones)
            .WithOne(org => org.Organization)
            .HasForeignKey(fk => fk.OrganizationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
