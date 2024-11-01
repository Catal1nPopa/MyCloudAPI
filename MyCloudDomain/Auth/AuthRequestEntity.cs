using System.Security.Cryptography;

namespace MyCloudDomain.Auth
{
    public class AuthRequestEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public byte[] Salt { get; set; }
        public string Role { get; set; }

        public AuthRequestEntity() { }
        public AuthRequestEntity(string username, string password, byte[] salt, string role)
        {
            UserName = username;
            Password = password;
            Salt = salt;
            Role = role;
        }
    }
}
