namespace UserLoginFormTest.Data.Commands;

public class DeleteCommand : IDeleteCommand
{
    public const string DeleteSql = @"DELETE FROM Users WHERE Email = @email";

    private AdoConnectionString _connectionString;

    public DeleteCommand(AdoConnectionString connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task Execute(string email, CancellationToken token)
    {
        using var sqlConnection = new SqlConnection(_connectionString.Value);
        using var c = new SqlCommand(DeleteSql, sqlConnection);
        await sqlConnection.OpenAsync(token);

        c.Parameters.AddWithValue("@email", email);

        await c.ExecuteNonQueryAsync(token);
        await sqlConnection.CloseAsync();
    }
}

public interface IDeleteCommand
{
    Task Execute(string email, CancellationToken token = default);
}
