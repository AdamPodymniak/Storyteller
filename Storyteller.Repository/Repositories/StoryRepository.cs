using Storyteller.Repository.Entities;

namespace Storyteller.Repository.Repositories
{
    public interface IStoryRepository : IGenericRepository<Story>
    {

    }
    public class StoryRepository : GenericRepository<Story>, IStoryRepository
    {
    }
}
