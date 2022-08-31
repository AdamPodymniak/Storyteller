using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Storyteller.API.Models;
using Storyteller.Repository.Entities;
using Storyteller.Repository.Repositories;

namespace Storyteller.API.Services
{
    public interface IStoryService
    {
        void AddStory(StoryRequestModel model);
        Story GetStory(Guid key);

    }
    public class StoryService : IStoryService
    {
        private readonly Microsoft.Extensions.Hosting.IHostingEnvironment _hostingEnv;
        private readonly IUserRepository _userRepository;
        private readonly IStoryRepository _storyRepository;
        private readonly ISlideRepository _slideRepository;
        private readonly ITextRepository _textRepository;
        public StoryService(
            IStoryRepository storyRepository,
            ISlideRepository slideRepository,
            ITextRepository textRepository,
            IUserRepository userRepository,
            Microsoft.Extensions.Hosting.IHostingEnvironment hostingEnv)
        {
            _userRepository = userRepository;
            _storyRepository = storyRepository;
            _slideRepository = slideRepository;
            _textRepository = textRepository;
            _hostingEnv = hostingEnv;
        }
        public void AddStory(StoryRequestModel model)
        {
            Story story = new Story();
            story.Name = model.Name;
            story.Description = model.Description;
            var fileName = "Image_" + DateTime.UtcNow.TimeOfDay.Milliseconds + model.File.FileName;
            var path = Path.Combine("", _hostingEnv.ContentRootPath+"Images/"+fileName);
            using(var stream = new FileStream(path, FileMode.Create))
            {
                model.File.CopyTo(stream);
            }
            story.ImgPath = path;
            story.UserGuid = _userRepository.GetByGuid(model.UserGuid).Guid;
            Guid key = Guid.NewGuid();
            story.StoryGuid = key;
            story.CreatedOn = DateTime.UtcNow;
            story.Rating = 0;
            _storyRepository.Add(story);
        }

        public Story GetStory(Guid key)
        {
            return _storyRepository.GetStoryByGuid(key);
        }
    }
}
