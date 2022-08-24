using Storyteller.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storyteller.Repository.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {

    }
    public class UsersRepository : GenericRepository<User>, IUserRepository
    {

    }
}
