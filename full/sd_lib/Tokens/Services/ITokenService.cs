namespace SD_lib.Tokens.Services.Interfaces
{
    public interface ITokenService
    {
        string Authenticate(string user, string password);
    }
}

