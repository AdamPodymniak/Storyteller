using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Storyteller.Repository.Entities;
using Storyteller.Repository.Repositories;

namespace Storyteller.API.Services
{
    public interface IStoryService
    {
        void AddStory(string name, string description);

    }
    public class StoryService : IStoryService
    {
        private readonly IStoryRepository _storyRepository;
        private readonly ISlideRepository _slideRepository;
        private readonly ITextRepository _textRepository;
        public StoryService(IStoryRepository storyRepository, ISlideRepository slideRepository, ITextRepository textRepository)
        {
            _storyRepository = storyRepository;
            _slideRepository = slideRepository;
            _textRepository = textRepository;
        }
        public void AddStory(string name, string description)
        {
            Story story = new Story();
            story.Name = name;
            story.Description = description;
            Guid key = Guid.NewGuid();
            story.UserGuid = key;
            story.CreatedOn = DateTime.UtcNow;
            story.Rating = 0;
            _storyRepository.Add(story);
        }
    }
}
