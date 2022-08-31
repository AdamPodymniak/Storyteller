using Dapper;
using Npgsql;
using Storyteller.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Storyteller.Repository.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User GetByName(string name);
        User GetByGuid(Guid key);
    }
    public class UsersRepository : GenericRepository<User>, IUserRepository
    {
        public User GetByGuid(Guid key)
        {
            using (var connection = new NpgsqlConnection(dbConnection))
            {
                SimpleCRUD.SetDialect(SimpleCRUD.Dialect.PostgreSQL);
                connection.Open();
                return connection.Get<User>(key);
            }
        }

        public User GetByName(string name)
        {
            using (var connection = new NpgsqlConnection(dbConnection))
            {
                User user = new User();
                SimpleCRUD.SetDialect(SimpleCRUD.Dialect.PostgreSQL);
                connection.Open();
                var types = connection.GetList<User>(new {Username = name});
                foreach(var type in types)
                {
                    user = type;
                }
                return user;
            }
        }
    }
}
