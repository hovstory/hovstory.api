using DAOLibrary.Repository;
using HOVStoryConfiguration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HOVStory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository userRepository;

        public UserController()
        {
            userRepository = new UserRepository();
        }

        // GET api/user/?email=abcxyz
        [HttpGet]
        [ProducesResponseType(500)]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(DTOLibrary.User), 200)]
        public IActionResult Get([FromQuery] string email)
        {
            try
            {
                DTOLibrary.User tempUser = userRepository.Get(email);
                if (tempUser == null)
                {
                    return StatusCode(204);
                }

                DTOLibrary.User user = new DTOLibrary.User
                {
                    Id = tempUser.Id,
                    Email = tempUser.Email,
                    Name = tempUser.Name
                };

                return StatusCode(200, user);
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/user/login
        [HttpPost("login")]
        [ProducesResponseType(401)]
        [ProducesResponseType(200)]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] DTOLibrary.User loginUser)
        {
            try
            {
                DTOLibrary.User user = userRepository.Login(loginUser.Email, loginUser.Password);

                if (user == null)
                {
                    throw new Exception("User does not exist!!");
                }

                user.Password = "";

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Globally Unique Identifier
                };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.Secret));

                // Token
                var token = new JwtSecurityToken(
                    issuer: Configuration.ValidIssuer,
                    audience: Configuration.ValidAudience,
                    expires: DateTime.Now.AddHours(2),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
                var accessToken = await HttpContext.GetTokenAsync("Bearer", "access_token");

                return StatusCode(200, new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            } catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        // POST api/user
        [HttpPost]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(DTOLibrary.User), 200)]
        [Authorize]
        public IActionResult SignUp([FromBody] DTOLibrary.User signUpUser)
        {
            try
            {
                string password = signUpUser.Password;
                signUpUser.Password = BCrypt.Net.BCrypt.HashPassword(password);

                DTOLibrary.User user = userRepository.SignUp(signUpUser);

                user.Password = "";
                return StatusCode(200, user);
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
