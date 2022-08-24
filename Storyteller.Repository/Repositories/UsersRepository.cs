﻿using Dapper;
using Npgsql;
using Storyteller.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storyteller.Repository.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User GetByGuid(Guid guid);
    }
    public class UsersRepository : GenericRepository<User>, IUserRepository
    {
        public User GetByGuid(Guid guid)
        {
            using (var connection = new NpgsqlConnection(dbConnection))
            {
                User user = new User();
                SimpleCRUD.SetDialect(SimpleCRUD.Dialect.PostgreSQL);
                connection.Open();
                var types = connection.GetList<User>();
                foreach(var type in types)
                {
                    user = type;
                }
                return user;
            }
        }
    }
}
