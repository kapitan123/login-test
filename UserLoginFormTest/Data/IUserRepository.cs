namespace UserLoginFormTest.Data;

public interface IUserRepository
{
    Task Create(string email, string password, CancellationToken token = default);

    Task<bool> CheckUserByEmail(string email, CancellationToken token = default);
}