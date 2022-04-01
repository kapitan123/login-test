namespace UserLoginFormTest.Services.Encryption
{
    public interface IEncryptionService
    {
        public string Hashify(string rawPassword, byte[] salt);

        public byte[] GenerateSalt();
    }
}
