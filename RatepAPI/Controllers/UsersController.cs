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
        private IConfiguration _config;

        public UsersController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(string CryptedLogin, string CryptedPassword)
        {
            VeloRaContext DBContext = new VeloRaContext();
            User user;
            try
            {
                user = DBContext.Users.ToList().FirstOrDefault(c => Security.StringToSHA256(c.Login) == CryptedLogin
                                                    && Security.StringToSHA256(c.Password) == CryptedPassword);
            }
            catch (Exception ex)
            {
                return NotFound(404);
            }

            if (user != null)
            {
                string token = GenerateToken(user);

                return Ok(token);
            }
            return NotFound(405);
        }

        private string GenerateToken(User user)
        {
            VeloRaContext DBContext = new VeloRaContext();
            Employee employee = DBContext.Employees.FirstOrDefault(c => c.Account == user);
            employee = DataForming.GetEmployeeData(employee);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, employee.PassportDatum.FirstName),
                new Claim(ClaimTypes.Surname, employee.PassportDatum.LastName),
                new Claim(ClaimTypes.MobilePhone, employee.PhoneNumber),
                new Claim(ClaimTypes.Role, employee.Post.Role.Name),
                new Claim(ClaimTypes.SerialNumber, $"{employee.PassportDatum.Seria} {employee.PassportDatum.Number}")
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
