namespace Library.Configs;

public class GlobalConfig
{
    public bool Test { get; set; }
    public JwtConfig Jwt { get; set; } = null!;
}