using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Storyteller.API.Services;

namespace Storyteller.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoryEditorController : ControllerBase
    {
        private readonly IStoryService _storyService;
        public StoryEditorController(IStoryService storyService)
        {
            _storyService = storyService;
        }
        [HttpPost("add")]
        public async Task<ActionResult> AddStory(string Name, string Description)
        {
            if (Name == null) return BadRequest("NameErr");
            _storyService.AddStory(Name, Description);
            return Ok("Story added");
        }
    }
}
