using Dapper;
using Microsoft.VisualBasic.FileIO;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storyteller.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public readonly string dbConnection = "Host=localhost;Username=postgres;Password=pasztet14;Database=Storyteller";
        public void Add(T obj)
        {
            using (var connection = new NpgsqlConnection(dbConnection))
            {
                SimpleCRUD.SetDialect(SimpleCRUD.Dialect.PostgreSQL);
                connection.Open();
                connection.Insert<T>(obj);
            }
        }

        public void Delete(int ID)
        {
            using (var connection = new NpgsqlConnection(dbConnection))
            {
                SimpleCRUD.SetDialect(SimpleCRUD.Dialect.PostgreSQL);
                connection.Open();
                connection.Delete<T>(ID);
            }
        }

        public T Get(int ID)
        {
            using (var connection = new NpgsqlConnection(dbConnection))
            {
                SimpleCRUD.SetDialect(SimpleCRUD.Dialect.PostgreSQL);
                connection.Open();
                var type = connection.Get<T>(ID);
                return type;
            }
        }

        public IEnumerable<T> GetList()
        {
            using (var connection = new NpgsqlConnection(dbConnection))
            {
                SimpleCRUD.SetDialect(SimpleCRUD.Dialect.PostgreSQL);
                connection.Open();
                var types = connection.GetList<T>();
                return types;
            }
        }

        public void Update(T obj)
        {
            using (var connection = new NpgsqlConnection(dbConnection))
            {
                SimpleCRUD.SetDialect(SimpleCRUD.Dialect.PostgreSQL);
                connection.Open();
                connection.Update<T>(obj);
            }
        }
    }
}
