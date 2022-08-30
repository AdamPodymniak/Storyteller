using Storyteller.Repository.Entities;

namespace Storyteller.Repository.Repositories
{
    public interface ITextRepository : IGenericRepository<Text>
    {

    }
    public class TextRepository : GenericRepository<Text>, ITextRepository
    {
    }
}
