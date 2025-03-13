namespace tj.SimpleBookStore.Services.Interface
{
    public interface IAuthenticationService
    {
        string GenerateToken(string username);
    }
}
