using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace UserLoginFormTest.Services.Encryption;

public class EncryptionService: IEncryptionService, IDisposable
{
    private readonly RandomNumberGenerator _rngGenerator;

    public EncryptionService()
    {
        _rngGenerator = RandomNumberGenerator.Create();
    }

    public byte[] GenerateSalt()
    {
        var salt = new byte[128 / 8];

        _rngGenerator.GetNonZeroBytes(salt);

        return salt;
    }

    public string Hashify(string rawPassword, byte[] salt)
    {
        // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
        return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: rawPassword,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
    }

    public void Dispose()
    {
        _rngGenerator.Dispose();
    }

}
