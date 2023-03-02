using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Training.MultiTenancy.Api.Services;

namespace Training.MultiTenancy.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly ITokenService _tokenService;

    public AuthenticationController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [AllowAnonymous]
    [HttpGet("signin")]
    public IActionResult SignIn(string userName, int tenantId)
    {
        if (string.IsNullOrEmpty(userName) || tenantId <= 0)
        {
            return BadRequest("userName or tenantId is empty.");
        }
        var token = _tokenService.GenerateAccessToken(userName, tenantId);
        return Ok(token);
    }
}
