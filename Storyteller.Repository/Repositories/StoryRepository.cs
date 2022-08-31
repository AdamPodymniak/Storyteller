using Dapper;
using Npgsql;
using Storyteller.Repository.Entities;

namespace Storyteller.Repository.Repositories
{
    public interface IStoryRepository : IGenericRepository<Story>
    {
        Story GetStoryByGuid(Guid key);
    }
    public class StoryRepository : GenericRepository<Story>, IStoryRepository
    {
        public Story GetStoryByGuid(Guid key)
        {
            using (var connection = new NpgsqlConnection(dbConnection))
            {
                SimpleCRUD.SetDialect(SimpleCRUD.Dialect.PostgreSQL);
                connection.Open();
                var type = connection.Get<Story>(key);
                return type;
            }
        }
    }
}
