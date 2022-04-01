namespace UserLoginFormTest.Data;

public class UserRepositoryADO : IUserRepository
{
    private readonly ICreateCommand _createCommand;
    private readonly IDeleteCommand _deleteCommand;
    private readonly IGetFirstQuery _getFirstQuery;
    private readonly IEncryptionService _encryptionService;

    public UserRepositoryADO(
        ICreateCommand createCommand, 
        IGetFirstQuery getFirstQuery,
        IDeleteCommand deleteCommand,
        IEncryptionService encryptionService)
    {
        _deleteCommand = deleteCommand ?? throw new ArgumentNullException(nameof(deleteCommand));
        _createCommand = createCommand ?? throw new ArgumentNullException(nameof(createCommand));
        _getFirstQuery = getFirstQuery ?? throw new ArgumentNullException(nameof(getFirstQuery));
        _encryptionService = encryptionService ?? throw new ArgumentNullException(nameof(encryptionService));
    }

    public async Task Create(string email, string password, CancellationToken token = default)
    {
        CheckEmail(email);

        var salt = _encryptionService.GenerateSalt();
        var hashedPassword = _encryptionService.Hashify(password, salt);

        await _createCommand.Execute(email, hashedPassword, System.Text.Encoding.UTF8.GetString(salt), token);
    }

    public async Task<bool> CheckUserByEmail(string email, CancellationToken token = default)
    {
        CheckEmail(email);

        return await _getFirstQuery.Execute(email, token);
    }

    private static void CheckEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email is mandatory for this operation");
        }
    }

    public async Task DeleteUserByEmail(string email, CancellationToken token = default)
    {
        CheckEmail(email);

        await _deleteCommand.Execute(email, token);
    }
}