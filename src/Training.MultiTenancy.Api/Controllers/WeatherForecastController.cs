using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Training.MultiTenancy.Data;

namespace Training.MultiTenancy.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly TenantInfo _tenantInfo;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, TenantInfo tenantInfo)
    {
        _logger = logger;
        _tenantInfo = tenantInfo;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            TenantId = _tenantInfo.TenantId,
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}