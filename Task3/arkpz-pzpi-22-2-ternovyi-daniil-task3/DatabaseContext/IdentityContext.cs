using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ChefMate_backend.Models;

public class IdentityContext : IdentityDbContext<ChefMateUser, IdentityRole<Guid>, Guid>
{
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
