using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Storyteller.API.Models;
using Storyteller.API.Services;
using Storyteller.Repository.Entities;
using Storyteller.Repository.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Storyteller.API.Controllers
{
    public class AuthController : ControllerBase
    {

        public static User user = new User();

        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;

        public AuthController(IConfiguration configuration, IAuthService authService)
        {
            _authService = authService;
            _configuration = configuration;
        }

        [HttpPost("api/register")]
        public async Task<ActionResult<User>> Register(UserRegistrationModel request)
        {
            if (request.Password.Length < 8) return BadRequest("Password is too short");
            if (request.Password != request.RepeatPassword) return BadRequest("Repeat Password Error");
            if (request.Invitation == null) return BadRequest("No invitation");

            string user = _authService.Register(request);
            if (user == "UsrErr") return BadRequest("Username is already taken");
            if (user == "InvErr") return BadRequest("Wrong invitation");

            return Ok(user);
        }

        [HttpPost("api/login")]
        public async Task<ActionResult<string>> Login(UserLoginModel request)
        {
            string response = _authService.Login(request);
            if (response == "UsrErr") return BadRequest("User does not exist");
            if (response == "PasErr") return BadRequest("Wrong password");
            return Ok(response);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpPost("api/getinvitation")]
        public async Task<ActionResult<string>> GetInvitation(string role)
        {
            string invitation = _authService.GenerateInvitation(role);
            return Ok(invitation);
        }

    }
}
