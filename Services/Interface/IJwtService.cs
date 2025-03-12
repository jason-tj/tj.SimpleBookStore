namespace tj.SimpleBookStore.Services.Interface
{
    public interface IJwtService
    {
        string GenerateToken(string username);
    }
}
