using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        HRMSContext db = new HRMSContext();
        private IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }
        // GET: api/Login


        [HttpGet]
        [Route("/dashboard")]
        
        public IActionResult Auth([FromHeader] String token)
        {
            return Ok(new { message = "Token is valid" });
        }

        // POST: api/Login
        [HttpPost]
        [Route("/Login")]
       
        // Credentials are recieved through header
        public IActionResult Login([FromBody] Users user)
        {
            IActionResult response = Unauthorized();
            var userName = user.EmailId;
            var password = user.Password;
            int id = GetHashCode();
            Users UserInfoDataBase = db.Users.FirstOrDefault(a => a.EmailId == userName);
            if (UserInfoDataBase != null && UserInfoDataBase.EmailId == userName)
            {
                if (UserInfoDataBase.Password == password)
                {
                    var tokenString = BuildToken(user);
                    response = Ok(new { token = tokenString,
                        status = "200",
                        message = "done"
                    });
                }
                else
                {
                    return response;
                }
            }
            else if(UserInfoDataBase == null)
            {
                throw new ArgumentNullException();
            }

            return response;
        }

        private string BuildToken(Users user)
        {

            var claims = new[] {
        new Claim(JwtRegisteredClaimNames.GivenName, user.EmailId),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
