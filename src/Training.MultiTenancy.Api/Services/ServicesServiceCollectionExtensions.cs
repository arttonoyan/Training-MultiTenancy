using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Training.MultiTenancy.Api.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServicesServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
        => services.AddScoped<ITokenService, TokenService>();

    public static IServiceCollection AddApplicationOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtTokenOptions>(o =>
        {
            var section = configuration.GetSection(JwtTokenOptions.Section);
            var key = Encoding.ASCII.GetBytes(section[nameof(JwtTokenOptions.Secret)]);
            o.Secret = key;
            o.AccessTokenDurationInMinutes = Convert.ToInt32(section[nameof(JwtTokenOptions.AccessTokenDurationInMinutes)]);
            o.AccessTokenDurationInMinutesRememberMe = Convert.ToInt32(section[nameof(JwtTokenOptions.AccessTokenDurationInMinutesRememberMe)]);
        });

        return services;
    }

    public static IServiceCollection AddAuthenticationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection(JwtTokenOptions.Section);
        var secret = Encoding.ASCII.GetBytes(section[nameof(JwtTokenOptions.Secret)]);

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = "bearer";
        }).AddJwtBearer("bearer", options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secret),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                    {
                        context.Response.Headers.Add("Token-Expired", "true");
                    }
                    return Task.CompletedTask;
                }
            };
        });

        services.AddAuthorization();

        return services;
    }

    public static IServiceCollection AddSwaggerLayer(this IServiceCollection services)
    {
        services.AddSwaggerGen(
           options =>
           {
               options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
               {
                   Name = "Authorization",
                   Type = SecuritySchemeType.ApiKey,
                   BearerFormat = "JWT",
                   In = ParameterLocation.Header,
                   Description = "JWT Authorization header using the Bearer scheme."
               });
               options.AddSecurityRequirement(new OpenApiSecurityRequirement()
               {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                                Scheme = "",
                                Name = "Bearer",
                                In = ParameterLocation.Header,
                            },
                            new List<string>()
                        }
               });
           });

        return services;
    }
}
