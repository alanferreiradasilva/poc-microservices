using Microsoft.EntityFrameworkCore;

namespace MicroShopPOC.AuthApi;

public class AppAuthDbContext(DbContextOptions<AppAuthDbContext> options) : DbContext(options)
{
    public DbSet<AppUser> Users => Set<AppUser>();
}
