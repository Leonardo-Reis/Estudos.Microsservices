using Microsoft.EntityFrameworkCore;

namespace Estudos.Microsservices.Infra.Data.SqlServer;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<BusMessageSqlDb> BusMessage { get; set; }
}
