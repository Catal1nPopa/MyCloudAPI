using System.Security.Cryptography;

namespace MyCloudDomain.Auth
{
    public class CreateUserLoginEntitiy
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Salt { get; set; }
        public DateTime CreateDate { get; } = DateTime.Now;

        public bool CheckPassword(string password)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, Convert.FromHexString(Salt), 350000, HashAlgorithmName.SHA512, 32);
            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(Password));
        }
    }
}
