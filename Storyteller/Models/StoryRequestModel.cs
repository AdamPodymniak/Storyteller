namespace Storyteller.API.Models
{
    public class StoryRequestModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile File { get; set; }
        public Guid UserGuid { get; set; }
    }
}
