namespace UserLoginFormTest.UnitTests.Data;

public class UserRepositoryADOTests
{
    private EncryptionService _encryptionService;
    private UserRepositoryADO _userRepo;
    private string _testEmail = "test@test.com";

    [SetUp]
    public void Setup()
    {
        var cs = new AdoConnectionString("Server=localhost;Database=TestDb;Trusted_Connection=True;MultipleActiveResultSets=true");

        var createCommand = new CreateCommand(cs);
        var getFirstQuery = new GetFirstQuery(cs);
        var deleteCommand = new DeleteCommand(cs);

        _encryptionService = new EncryptionService();
        _userRepo = new UserRepositoryADO(createCommand, getFirstQuery, deleteCommand, _encryptionService);
    }

    [Test]
    public async Task Should_Not_Allow_User_With_The_Same_Name()
    {
        await _userRepo.Create(_testEmail, "testpassword");

        var isPresent = await _userRepo.CheckUserByEmail(_testEmail);

        isPresent.Should().Be(true);
    }

    [TearDown]
    public async Task TearDown()
    {
        await _userRepo.DeleteUserByEmail(_testEmail);

        _encryptionService.Dispose();
    }
}
