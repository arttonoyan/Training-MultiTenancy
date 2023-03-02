using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Training.MultiTenancy.Api.Metada;
using Training.MultiTenancy.Data;

namespace Training.MultiTenancy.Api.Middlwares
{
    public class TenantResolutionMiddlware : IMiddleware
    {
        private readonly TenantInfo _tenantInfo;

        public TenantResolutionMiddlware(TenantInfo tenantInfo)
        {
            _tenantInfo = tenantInfo;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context == null)
                return;

            var endpoint = context.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
            {
                await next(context);
                return;
            }

            string authHeader = context.Request.Headers["Authorization"];
            var token = authHeader!.Replace("Bearer ", string.Empty).Replace("bearer ", string.Empty);

            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var tenantClaim = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == ApplicationClaims.TenantId);

            if (tenantClaim == null)
            {
                throw new ArgumentNullException(nameof(ApplicationClaims.TenantId));
            }

            if (int.TryParse(tenantClaim.Value, out int id))
                _tenantInfo.TenantId = id;
            else
                throw new FormatException($"{nameof(ApplicationClaims.TenantId)} - wrong format");

            await next(context);
        }
    }
}
