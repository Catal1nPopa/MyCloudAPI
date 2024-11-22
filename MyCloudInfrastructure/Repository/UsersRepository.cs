using Microsoft.EntityFrameworkCore;
using MyCloudDomain.Auth;
using MyCloudDomain.Interfaces;

namespace MyCloudInfrastructure.Repository
{
    public class UsersRepository(MyDbContext myDbContext) : IUsersRepository
    {
        private readonly MyDbContext _myDbContext = myDbContext;
        public async Task<UserEntity> GetUser(string userEmail)
        {
            UserEntity? user = await _myDbContext.users.FirstOrDefaultAsync(user => user.Email == userEmail);
            return user;
        }

        public async Task<List<UserEntity>> GetUsers()
        {
            return await _myDbContext.users.ToListAsync();
        }
    }
}
