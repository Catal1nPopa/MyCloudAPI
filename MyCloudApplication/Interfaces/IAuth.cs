using MyCloudApplication.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCloudApplication.Interfaces
{
    public interface IAuth
    {
        Task<string> getAuthentication(AuthRequestDTO authRequest);
        Task<bool> CreateUserLogin(CreateUserLoginDTO createUserLogin);
    }
}
