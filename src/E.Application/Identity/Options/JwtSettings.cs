namespace E.Application.Identity.Options;

public class JwtSettings
{
    public string SigningKey { get; set; }
    public string Issuer { get; set; }
    public List<string> Audiences { get; set; }
}
