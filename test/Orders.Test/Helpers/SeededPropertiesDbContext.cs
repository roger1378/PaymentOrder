using Microsoft.EntityFrameworkCore;
using Orders.Repository;

namespace Orders.Test.Helpers;

public static class SeededPaymentOrdersDbContext
{
    public static PaymentOrdersDbContext
       BuildPaymentOrdersDbContext()
    {
        var dbContext = new PaymentOrdersDbContext(
            new DbContextOptionsBuilder<PaymentOrdersDbContext>()
                .UseInMemoryDatabase($"PaymentOrdersDb-{Guid.NewGuid():N}", configuration => configuration.UseHierarchyId())
                .Options);

        return dbContext;
    }

    public static PaymentOrdersDbContext
       BuildPaymentOrdersDbContext(string databaseName)
    {
        var dbContext = new PaymentOrdersDbContext(
            new DbContextOptionsBuilder<PaymentOrdersDbContext>()
                .UseInMemoryDatabase(databaseName, configuration => configuration.UseHierarchyId())
                .Options);

        return dbContext;
    }

    public static async Task<PaymentOrdersDbContext>
        BuildPaymentOrdersDbContextAsync<TEntity>(IEnumerable<TEntity> entities)
        where TEntity : class
    {
        var dbContext = BuildPaymentOrdersDbContext();

        await dbContext.Set<TEntity>().AddRangeAsync(entities);
        await dbContext.SaveChangesAsync();

        return dbContext;
    }

    public static (PaymentOrdersDbContext dbContext, string databaseName)
       BuildPaymentOrdersDbContextWithDatabaseName()
    {
        var databaseName = $"PaymentOrdersDb-{Guid.NewGuid():N}";
        var dbContext = new PaymentOrdersDbContext(
            new DbContextOptionsBuilder<PaymentOrdersDbContext>()
                .UseInMemoryDatabase(databaseName, configuration => configuration.UseHierarchyId())
                .Options);

        return (dbContext, databaseName);
    }
}
