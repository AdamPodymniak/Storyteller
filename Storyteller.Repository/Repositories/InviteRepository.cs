using Dapper;
using Npgsql;
using Storyteller.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storyteller.Repository.Repositories
{
    public interface IInviteRepository : IGenericRepository<Invite>
    {
        IEnumerable<Invite> GetNotUsed();
    }
    public class InviteRepository : GenericRepository<Invite>, IInviteRepository
    {
        public IEnumerable<Invite> GetNotUsed()
        {
            using (var connection = new NpgsqlConnection(dbConnection))
            {
                SimpleCRUD.SetDialect(SimpleCRUD.Dialect.PostgreSQL);
                connection.Open();
                var types = connection.GetList<Invite>(new {Used = false});
                return types;
            }
        }
    }
}
