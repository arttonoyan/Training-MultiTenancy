using Training.MultiTenancy.Api.Middlwares;

namespace Microsoft.AspNetCore.Builder;

public static partial class MultiTenancyApplicationBuilderExtensions
{
    public static IApplicationBuilder UseMultiTenancy(this IApplicationBuilder app)
        => app.UseMiddleware<TenantResolutionMiddlware>();
}
