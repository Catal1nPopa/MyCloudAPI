using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCloudAPI.Models;
using MyCloudApplication.DTOs.User;
using MyCloudApplication.Interfaces;

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
            var token = await _auth.getAuthentication(authRequest.Adapt<AuthRequestDTO>());
            return Ok(new { token });
        }

        [Authorize]
        [HttpPost("AddLogin")]
        public async Task<IActionResult> AddUserLogin([FromBody] CreateUserLoginModel createUserLogin)
        {
            return Ok(await _auth.CreateUserLogin(createUserLogin.Adapt<CreateUserLoginDTO>()));
        }
    }
}
