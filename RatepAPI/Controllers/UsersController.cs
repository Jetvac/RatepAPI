#nullable disable
using Microsoft.AspNetCore.Mvc;
using RatepAPI.Models;
using RatepAPI.Class;

namespace RatepAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly VeloRaContext _context;

        public UsersController()
        {
            _context = new VeloRaContext();
        }

        [HttpPost("Login")]
        public ActionResult<string> Login(string login, string password)
        {
            string CryptPassword = Security.StringToSHA256(password);
            var user = _context.Users.FirstOrDefault(c => c.Login == login && c.Password == CryptPassword);

            if (user != null)
            {
                string token = Security.GenerateToken(login, password);

                if (user.Token != token)
                {
                    try
                    {
                        user.Token = token;
                        _context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        return NotFound(404);
                    }
                }
                return token;
            }
            return NotFound(405);
        }
    }
}
