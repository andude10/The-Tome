using Microsoft.EntityFrameworkCore;
using TheTome.Infrastructure.Data;

namespace IntegrationTests;

public class TestDbContextFactory : IDbContextFactory<AppDbContext>
{
    private readonly DbContextOptions<AppDbContext> _options;

    public TestDbContextFactory(string databaseName = "InMemoryTest")
    {
        _options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName)
            .Options;
    }

    public AppDbContext CreateDbContext()
    {
        return new AppDbContext(_options);
    }
}