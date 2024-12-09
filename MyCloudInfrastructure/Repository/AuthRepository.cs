using Microsoft.EntityFrameworkCore;
using MyCloudDomain.Auth;
using MyCloudDomain.Interfaces;

namespace MyCloudInfrastructure.Repository
{
    public class AuthRepository(MyDbContext myDbContext) : IAuthRepository
    {
        private readonly MyDbContext _context = myDbContext;

        public async Task<bool> createUserLogin(CreateUserLoginEntitiy createUser)
        {
            try
            {
                _context.user_credentials.Add(createUser);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<CreateUserLoginEntitiy> getUserByUsername(string username)
        {
            //return await _context.AuthEntities.FindAsync(1);   
            return await _context.user_credentials.FirstOrDefaultAsync(user => user.UserName == username);
        }
    }
}
