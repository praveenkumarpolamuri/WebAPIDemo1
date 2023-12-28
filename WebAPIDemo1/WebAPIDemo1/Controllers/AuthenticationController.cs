using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthentocationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthentocationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            // For simplicity, assume user authentication logic here
            // Authenticate user against database or other data source
            bool isAuthenticated = await YourAuthenticateUserMethod(loginModel.Username, loginModel.Password);

            if (isAuthenticated)
            {
                var token = GenerateJwtToken(loginModel.Username);
                return Ok(new { token });
            }

            return Unauthorized("Invalid credentials");
        }

        // Sample method to authenticate a user (replace this with your actual authentication logic)
        private Task<bool> YourAuthenticateUserMethod(string username, string password)
        {
            // Replace this with your authentication logic (database check, external service call, etc.)
            // Return true if authentication is successful, else return false
            // For demo purposes, let's assume a hardcoded check
            return Task.FromResult(username == "admin" && password == "password");
        }

        private string GenerateJwtToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:ValidIssuer"],
                audience: _configuration["Jwt:ValidAudience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpirationInMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
