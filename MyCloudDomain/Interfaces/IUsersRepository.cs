using MyCloudDomain.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCloudDomain.Interfaces
{
    public interface IUsersRepository
    {
        Task<List<UserEntity>> GetUsers();
        Task<UserEntity> GetUser(string userEmail);
    }
}
