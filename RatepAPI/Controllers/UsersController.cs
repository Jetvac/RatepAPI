#nullable disable
using Microsoft.AspNetCore.Mvc;
using RatepAPI.Models;
using RatepAPI.Class;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace RatepAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        public static VeloRaContext DBContext;
        private IConfiguration _config;

        public UsersController(IConfiguration config)
        {
            _config = config;
            DBContext = new VeloRaContext();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(string CryptedLogin, string CryptedPassword)
        {
            User user;
            try
            {
                user = DBContext.Users.ToList().FirstOrDefault(c => Security.StringToSHA256(c.Login) == CryptedLogin
                                                    && Security.StringToSHA256(c.Password) == CryptedPassword);
            } catch (Exception ex)
            {
                return NotFound(404);
            }

            if (user != null)
            {
                string token = GenerateToken(user);

                if (user.Token != token)
                {
                    try
                    {
                        user.Token = token;
                        DBContext.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        return NotFound(404);
                    }
                }
                return Ok(token);
            }
            return NotFound(405);
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.FullName.Split(' ')[0]),
                new Claim(ClaimTypes.Surname, user.FullName.Split(' ')[1]),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
