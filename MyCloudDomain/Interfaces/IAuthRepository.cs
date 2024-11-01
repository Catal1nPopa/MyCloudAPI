using MyCloudDomain.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCloudDomain.Interfaces
{
    public interface IAuthRepository
    {
        Task<CreateUserLoginEntitiy> getUserByUsername(string username);
        Task<bool> createUserLogin(CreateUserLoginEntitiy createUser);
    }
}
