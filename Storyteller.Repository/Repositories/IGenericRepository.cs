using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storyteller.Repository.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetList();
        T Get(int ID);
        void Add(T obj);
        void Update(T obj);
        void Delete(int ID);
    }
}
