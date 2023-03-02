using Microsoft.EntityFrameworkCore;
using Training.MultiTenancy.Data;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class DataServiceCollectionExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services">Registers the given <see cref="TenantDbCntext" /> as a service in the <see cref="IServiceCollection" />.</param>
    /// <param name="connectionString">The connection string of the database to connect to.</param>
    /// <param name="logAction">Delegate called when there is a message to log.</param>
    /// <returns></returns>
    public static IServiceCollection AddMultiTenancyData(this IServiceCollection services, string connectionString, Action<string>? logAction = null) =>
        services
            .AddScoped<TenantInfo>()
            .AddDbContext<TenantDbCntext>(options => {
                options.UseSqlServer(connectionString);
                if (logAction != null)
                {
                    options.LogTo(logAction);
                }
            });
}
