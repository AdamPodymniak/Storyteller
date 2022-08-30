using Storyteller.Repository.Entities;

namespace Storyteller.Repository.Repositories
{
    public interface ISlideRepository : IGenericRepository<Slide>
    {

    }
    public class SlideRepository : GenericRepository<Slide>, ISlideRepository
    {
    }
}
