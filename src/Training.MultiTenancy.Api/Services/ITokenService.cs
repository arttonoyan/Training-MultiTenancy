namespace Training.MultiTenancy.Api.Services;

public interface ITokenService
{
    string GenerateAccessToken(string userName, int tenantId);
}
