using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Estudos.Microsservices.Infra.Data.SqlServer;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        optionsBuilder.UseSqlServer(
            "Server=localhost,1433;Database=Estudos.Microsservicos;User Id=sa;Password=SeuStrong!Passw0rd;TrustServerCertificate=True;"
        );

        return new AppDbContext(optionsBuilder.Options);
    }
}