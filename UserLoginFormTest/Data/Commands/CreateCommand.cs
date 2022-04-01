namespace UserLoginFormTest.Data.Commands;

public class CreateCommand : ICreateCommand
{
    public const string CreateSql = @"INSERT INTO [Users] (Email, PasswordHash, Salt) VALUES (@email, @passwordHash, @salt)";

    private AdoConnectionString _connectionString;

    public CreateCommand(AdoConnectionString connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task Execute(string email, string passwordHash, string salt, CancellationToken token)
    {
        // We directly instatniate new connections so we don't mess with conection pooling
        using var sqlConnection = new SqlConnection(_connectionString.Value);
        using var c = new SqlCommand(CreateSql, sqlConnection);
        await sqlConnection.OpenAsync(token);

        // Best practice would be to specify datatypes there, but I chose a shorter syntax
        c.Parameters.AddWithValue("@email", email);
        c.Parameters.AddWithValue("@passwordHash", passwordHash);
        c.Parameters.AddWithValue("@salt", salt);

        await c.ExecuteNonQueryAsync(token);
        await sqlConnection.CloseAsync();
    }
}

public interface ICreateCommand
{
    Task Execute(string email, string passwordHash, string salt, CancellationToken token = default);
}
