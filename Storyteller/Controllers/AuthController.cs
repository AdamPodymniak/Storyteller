﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Storyteller.API.Models;
using Storyteller.API.Services;
using Storyteller.Repository.Entities;

namespace Storyteller.API.Controllers
{
    [EnableCors("CorsAPI")]
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

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
        public async Task<ActionResult<string>> Login(UserLoginModel request)
        {
            string response = _authService.Login(request);
            if (response == "UsrErr") return BadRequest("UsrErr");
            if (response == "PasErr") return BadRequest("PasErr");
            return Ok(response);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpPost("getinvitation")]
        public async Task<ActionResult<string>> GetInvitation(InvitationModel role)
        {
            string invitation = _authService.GenerateInvitation(role.Role);
            return Ok(invitation);
        }

    }
}
