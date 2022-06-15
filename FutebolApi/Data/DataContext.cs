using FutebolApi.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FutebolApi.Data;

public class DataContext : IdentityDbContext<IdentityUser>
{
    
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);      
    }

    public DbSet<Player> Players { get; set; }
}
