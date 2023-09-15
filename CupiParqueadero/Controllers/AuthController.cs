using CupiParqueadero.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.IdentityModel.Tokens;
using ParkingMentoring.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace ParkingMentoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        public static User user = new User();

        public readonly IConfiguration _configuration;
        private readonly DataContext _context;

        public AuthController(IConfiguration configuration, DataContext context) 
        {
            _configuration = configuration;
            _context = context;
        }


        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> Get()
        {
            List<User> lista = new List<User>();

            try
            {
                lista = _context.Users.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }
        }

        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            var auth = new Auth(_context, _configuration);
            auth.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.Username = request.Username;
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;

            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Usuario agregado con exito" });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = e.Message });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            var auth = new Auth(_context, _configuration);
            var user = _context.Users.FirstOrDefault(u => u.Username == request.Username);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            if (!auth.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong password");
            }
            string token = auth.CreateToken(user);
            return Ok(token);
        }
    }
}
