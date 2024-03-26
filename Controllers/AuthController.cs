using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineCasino.Logic.Interfaces;
using OnlineCasino.Models.Request;
using OnlineCasino.Models.Response;

namespace OnlineCasino.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/auth")]
    [ApiVersion("1.0")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUsersService _usersService;
        private readonly ILogger<GameController> _logger;

        public AuthController(IConfiguration configuration, IUsersService usersService, ILogger<GameController> logger)
        {
            _configuration = configuration;
            _usersService = usersService;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Authenticate([FromBody] LoginRequest request)
        {
            try
            {
                if (IsValidCredentials(request.UserName, request.Password))
                {
                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, request.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

                    var securityToken = GetToken(authClaims);

                    var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

                    HttpContext.Session.SetString("AuthToken", token);

                    var response = new LoginResponse
                    {
                        Status = Status.Success.ToString(),
                        Token = token,
                        Expiration = securityToken.ValidTo
                    };

                    return Ok(response);
                }
                else
                {
                    var response = new LoginResponse
                    {
                        Message = "Invalid username or password",
                        Status = Status.Failure.ToString(),
                    };

                    return BadRequest(response);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        private bool IsValidCredentials(string username, string password)
        {
            return _usersService.IsValidCredentials(username, password);
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }
    }
}
