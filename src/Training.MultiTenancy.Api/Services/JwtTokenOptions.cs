namespace Training.MultiTenancy.Api.Services;

public class JwtTokenOptions
{
    public const string Section = "JwtToken";

    public byte[] Secret { get; set; } = null!;
    public int AccessTokenDurationInMinutes { get; set; }
    public int AccessTokenDurationInMinutesRememberMe { get; set; }
}
