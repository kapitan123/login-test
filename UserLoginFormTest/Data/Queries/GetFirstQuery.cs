namespace UserLoginFormTest.Data.Queries;

public class GetFirstQuery : IGetFirstQuery
{
    private const string GetFirst = @"SELECT TOP 1 Email FROM [Users] WHERE Email = @email";

    private AdoConnectionString _connectionString;

    public GetFirstQuery(AdoConnectionString connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<bool> Execute(string email, CancellationToken token)
    {
        using var sqlConnection = new SqlConnection(_connectionString.Value);
        await sqlConnection.OpenAsync(token);

        using var c = new SqlCommand(GetFirst, sqlConnection);
        c.Parameters.AddWithValue("@email", email);

        using var reader = await c.ExecuteReaderAsync(token);
        var exists = false;
        if (await reader.ReadAsync(token))
        {
            exists = (string)reader["Email"] != null;
        }

        await sqlConnection.CloseAsync();
        return exists;
    }
}

public interface IGetFirstQuery
{
    Task<bool> Execute(string email, CancellationToken token = default);
}

