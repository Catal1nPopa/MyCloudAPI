namespace MyCloudAPI.Models
{
    public class CreateUserLoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
