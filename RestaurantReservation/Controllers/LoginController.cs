using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RestaurantReservation.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestaurantReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LogInDTO log)
        {
            //if (log.Username.IsNullOrEmpty() || log.Username.IsNullOrEmpty())
            //{
            //    return BadRequest("Enter username or password");
            //}
            if (log.Username=="string" && log.Password== "string")
            {
                var claims = new[]
                {
                     new Claim(JwtRegisteredClaimNames.Sub,_configuration["Jwt:Subject"]),
                     new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                     new Claim("username",log.Username.ToString()),
                     //new Claim("Role",employee.Role.ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(60),
                    signingCredentials: signIn
                    );
                string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new { Token = tokenValue });
            }
            else
            {
                return Unauthorized();
            }
        }
    }
 }