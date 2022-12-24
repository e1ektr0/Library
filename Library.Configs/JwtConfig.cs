namespace Library.Configs;

public class JwtConfig
{
    public const string OptionsSection = "Jwt";
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public string SecretKey { get; set; } = null!;
    public int ExpiresAccessTokenHours { get; set; }
    public int ExpiresRefreshTokenDays { get; set; }
}