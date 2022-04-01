namespace UserLoginFormTest.UnitTests.Data;

public class UserRepositoryADOTests
{
    private Mock<IEncryptionService> _encryptionServiceMock;
    private Mock<ICreateCommand> _createCommand;
    private Mock<IGetFirstQuery> _getFirstQuery;
    private Mock<IDeleteCommand> _deleteCommand;
    private string _defaultHash = "testhash";

    [SetUp]
    public void Setup()
    {
        _encryptionServiceMock = new Mock<IEncryptionService>();
        _encryptionServiceMock
            .Setup(e => e.Hashify(It.IsAny<string>(), It.IsAny<byte[]>()))
            .Returns(_defaultHash);

        _createCommand = new Mock<ICreateCommand>();
        _getFirstQuery = new Mock<IGetFirstQuery>();
        _deleteCommand = new Mock<IDeleteCommand>();
    }

    [Test]
    [TestCase(null), TestCase(""), TestCase("   ")]
    public async Task Should_Throw_On_User_Creation_When_Email_Is_Empty(string emptyEmail)
    {
        var userRepo = new UserRepositoryADO(_createCommand.Object, 
            _getFirstQuery.Object, _deleteCommand.Object, _encryptionServiceMock.Object);

        var act = async () => await userRepo.Create(emptyEmail, "test");

        await act.Should().ThrowAsync<ArgumentException>();
    }

    [Test]
    public async Task Should_Transform_Password_To_Hash_On_User_Creation()
    {
        var userRepo = new UserRepositoryADO(_createCommand.Object, 
            _getFirstQuery.Object, _deleteCommand.Object, _encryptionServiceMock.Object);

        await userRepo.Create("test@test.com", "testpassword");

        _createCommand.Verify(c => c.Execute(
            It.IsAny<string>(),
            It.Is<string>(p => p == _defaultHash),
            It.IsAny<string>(),
            new CancellationToken(false)),
            Times.Once);
    }
}

