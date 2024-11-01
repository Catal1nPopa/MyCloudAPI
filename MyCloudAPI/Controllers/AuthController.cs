using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCloudAPI.Models;
using MyCloudApplication.DTOs;
using MyCloudApplication.Interfaces;
using MyCloudHelper;

namespace MyCloudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuth authServices) : ControllerBase
    {
        private readonly IAuth _auth = authServices;

        [HttpPost("Login")]
        public async Task<IActionResult> Authentication([FromBody] AuthRequestModel authRequest)
        {
            return Ok(await _auth.getAuthentication(authRequest.Adapt<AuthRequestDTO>()));
        }

        [HttpPost("AddLogin")]
        public async Task<IActionResult> AddUserLogin([FromBody] CreateUserLoginModel createUserLogin)
        {
            return Ok(await _auth.CreateUserLogin(createUserLogin.Adapt<CreateUserLoginDTO>()));
        }
    }
}
