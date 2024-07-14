namespace E.API.Contracts.Tokens.Requests;

public class RefreshTokenRequest
{
    public string UserName { get; set; }
    public string RefreshToken { get; set; }
}
