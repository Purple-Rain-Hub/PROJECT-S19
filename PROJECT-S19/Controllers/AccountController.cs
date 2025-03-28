using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PROJECT_S19.Models;
using PROJECT_S19.Settings;
using PROJECT_S19.DTOs.Account;
using PROJECT_S19.Services;

namespace PROJECT_S19.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequest)
        {
            var success = await _accountService.RegisterAsync(registerRequest);
            if (success)
            {
                return Ok(new { message = "Account successfully registered!" });
            }
            else
            {
                return BadRequest(new { message = "Email is already registered or something went wrong." });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            var (success, result) = await _accountService.LoginAsync(loginRequest);

            if (success)
            {
                return Ok(new { token = result });
            }
            else
            {
                return Unauthorized(new { message = result });
            }
        }
    }
}
