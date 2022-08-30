using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Storyteller.API.Models;
using Storyteller.API.Services;
using Storyteller.Repository.Entities;

namespace Storyteller.API.Controllers
{
    [EnableCors("CorsAPI")]
    [Route("/api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        private const string Admin =  "4214564343";
        private const string Writer = "8546342134";
        private const string Reader = "0978441234";

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserRegistrationModel request)
        {
            if (request.Password.Length < 8) return BadRequest("PasErr1");
            if (request.Password != request.RepeatPassword) return BadRequest("PasErr2");
            if (request.Invitation == null) return BadRequest("InvErr");

            string user = _authService.Register(request);
            if (user == "UsrErr") return BadRequest("UsrErr");
            if (user == "InvErr") return BadRequest("InvErr");

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseTokenModel>> Login(UserLoginModel request)
        {
            var response = _authService.Login(request);
            if (response == null) return BadRequest("Err");
            return Ok(response);
        }
        [HttpPost("refresh")]
        public async Task<ActionResult<TokenModel>> RefreshToken(TokenModel tokenApiModel)
        {
            if (tokenApiModel is null)
                return BadRequest("Invalid client request");
            string accessToken = tokenApiModel.JWTToken;
            string refreshToken = tokenApiModel.RefreshToken;
            var principal = _authService.GetPrincipalFromExpiredToken(accessToken);
            string name = principal.Identity.Name;
            var user = _authService.GetUserFromName(name);
            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                return BadRequest("Invalid client request");

            var newJWTToken = _authService.CreateJWTToken(user);

            return Ok(new TokenModel
            {
                JWTToken = newJWTToken,
                RefreshToken = refreshToken,
            });
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Admin)]
        [HttpPost("getinvitation")]
        public async Task<ActionResult<string>> GetInvitation(InvitationModel role)
        {
            if (role.Role == "Admin") role.Role = Admin;
            else if (role.Role == "Writer") role.Role = Writer;
            else if (role.Role == "Reader") role.Role = Reader;
            else return BadRequest("Why are you even trying?");
            string invitation = _authService.GenerateInvitation(role.Role);
            return Ok(invitation);
        }

    }
}
