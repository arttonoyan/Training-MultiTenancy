using Training.MultiTenancy.Api.Middlwares;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class MultitenancyMiddlwareServiceCollectionExtesions
{
    public static IServiceCollection AddApplicationMiddlwares(this IServiceCollection services)
        => services.AddScoped<TenantResolutionMiddlware>();
}
