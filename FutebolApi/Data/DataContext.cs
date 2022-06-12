using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FutebolApi.Data;

public class DataContext : IdentityDbContext<IdentityUser>
{
    private readonly IConfiguration _configuration;
    public DataContext(DbContextOptions options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var adminId = _configuration["UserConfigurations:AdminId"];
        var roleId = _configuration["UserConfigurations:RoleId"];
        var email = _configuration["UserConfigurations:Email"];
        var password = _configuration["UserConfigurations:Password"];

        builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = roleId,
            Name = "admin",
            NormalizedName = "admin"
        });

        var hasher = new PasswordHasher<IdentityUser>();
        builder.Entity<IdentityUser>().HasData(new IdentityUser
        {
            Id = adminId,
            UserName = "admin",
            NormalizedUserName = "admin".ToUpper(),
            Email = email,
            NormalizedEmail = email.ToUpper(),
            EmailConfirmed = true,
            PasswordHash = hasher.HashPassword(null, password),
            SecurityStamp = string.Empty
        });

        builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            RoleId = roleId,
            UserId = adminId
        });
    }
}
