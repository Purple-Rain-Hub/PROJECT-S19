﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PROJECT_S19.DTOs.Account;
using PROJECT_S19.Models;
using PROJECT_S19.Settings;

namespace PROJECT_S19.Services
{
    public class AccountService
    {
        private readonly Jwt _jwtSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountService(IOptions<Jwt> jwtOptions,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _jwtSettings = jwtOptions.Value;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<bool> RegisterAsync(RegisterRequestDto registerRequest)
        {
            try
            {
                var newUser = new ApplicationUser()
                {
                    Email = registerRequest.Email,
                    UserName = registerRequest.Email,
                    FirstName = registerRequest.FirstName,
                    LastName = registerRequest.LastName
                };

                var result = await _userManager.CreateAsync(newUser, registerRequest.Password);

                if (!result.Succeeded)
                {
                    return false;
                }

                await _userManager.AddToRoleAsync(newUser, "User");

                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<(bool, string)> LoginAsync(LoginRequestDto loginRequest)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginRequest.Email);

                if (user == null)
                {
                    return (false, "Invalid email or password");
                }

                var result = await _signInManager.PasswordSignInAsync(user, loginRequest.Password, false, false);

                if (!result.Succeeded)
                {
                    return (false, "Invalid email or password");
                }

                var Roles = await _signInManager.UserManager.GetRolesAsync(user);

                List<Claim> claims = new List<Claim>();

                claims.Add(new Claim(ClaimTypes.Email, user.Email));
                claims.Add(new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"));
                foreach (var role in Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expiry = DateTime.Now.AddMinutes(_jwtSettings.ExpiresInMinutes);
                var token = new JwtSecurityToken(_jwtSettings.Issuer, _jwtSettings.Audience, claims, expires: expiry, signingCredentials: creds);
                string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return (true, tokenString);
            }
            catch
            {
                return (false, "An error occurred while processing the request");
            }
        }
    }
}
