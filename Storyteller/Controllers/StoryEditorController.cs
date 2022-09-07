﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Storyteller.API.Models;
using Storyteller.API.Services;
using Storyteller.Repository.Entities;
using System.Data;

namespace Storyteller.API.Controllers
{
    [EnableCors("CorsAPI")]
    [Route("api/[controller]")]
    [ApiController]
    public class StoryEditorController : ControllerBase
    {
        private const string Admin = "4214564343";
        private const string Writer = "8546342134";
        private const string Reader = "0978441234";

        private readonly IStoryService _storyService;
        public StoryEditorController(IStoryService storyService)
        {
            _storyService = storyService;
        }
        [HttpPost("add")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Admin)]
        public async Task<ActionResult> AddStory([FromForm]StoryRequestModel model)
        {
            if (model.Name == null) return BadRequest("NameErr");
            _storyService.AddStory(model);
            return Ok("Story added");
        }
        [HttpGet("get")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<Story>>> GetStory()
        {
            var story = _storyService.GetStory();
            if (story == null) return BadRequest("How?");
            return Ok(story);
        }
    }
}
