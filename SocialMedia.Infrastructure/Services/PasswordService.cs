using Microsoft.Extensions.Options;
using SocialMedia.Infrastructure.Interfaces;
using SocialMedia.Infrastructure.Options;
using System.Security.Cryptography;

namespace SocialMedia.Infrastructure.Services
{
    public class PasswordService(IOptions<PasswordOption> option) : IPasswordHasher
    {
        private readonly PasswordOption _options = option.Value;

        public bool Check(string hash, string password)
        {
            var parts = hash.Split(".",3);
            if(parts.Length != 3)
            {
                throw new FormatException("Unexpected hash Format");
            }
            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);
            //pBDKF" Implemantion
            using var algorithm = new Rfc2898DeriveBytes(
                password,
                salt,
                iterations,
                HashAlgorithmName.SHA512
                );
            var keyToCheck = algorithm.GetBytes(_options.KeySize);
            return keyToCheck.SequenceEqual(key);
           
        }

        public string Hash(string password)
        {
            //pBDKF" Implemantion
            using var algorithm = new Rfc2898DeriveBytes(
                password,
                _options.SaltSize,
                _options.Iterations,
                HashAlgorithmName.SHA512
                );
            var key = Convert.ToBase64String(algorithm.GetBytes(_options.KeySize));
            var salt = Convert.ToBase64String(algorithm.Salt);
            return string.Concat(_options.Iterations,".", salt,".", key);


        }
    }
}
