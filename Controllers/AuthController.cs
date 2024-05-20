using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PassCodeManager.DTO.RequestObjects;
using PassCodeManager.Services.Abstract;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PassCodeManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;
        private readonly IUserService _userService;
        public AuthController(IConfiguration configuration, ILogger<AuthController> logger, IUserService userService)
        {

            _configuration = configuration
                ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration
                ?? throw new ArgumentNullException(nameof(configuration));
            _userService = userService
                ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ApiResponse> Login(string userName, string password)
        {
            bool IsValidUser = await _userService.ValidateUser(userName, password);

            if (IsValidUser)
            {
                var token = GenerateJwtToken(new RegisterUserObject() { UserName = userName});
                return new ApiResponse(token);
            }
            return  new ApiResponse("User is Not Valid.");
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ApiResponse> Register(RegisterUserObject request)
        {
            try
            {
                var result = await _userService.RegisterUserAsync(request);
                var token = GenerateJwtToken(request);

                return new ApiResponse(token);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string GenerateJwtToken(RegisterUserObject request)
        {
            var jwtSettings = _configuration.GetSection("Jwt");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]?? string.Empty);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name,request.UserName),
                new Claim(ClaimTypes.Email,string.IsNullOrWhiteSpace(request.Email) ? string.Empty : request.Email)  

                // Role need to be implemented.
                }),
                Audience = jwtSettings["Audience"],
                Issuer = jwtSettings["Issuer"],
                Expires = DateTime.UtcNow.AddHours(1), 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
