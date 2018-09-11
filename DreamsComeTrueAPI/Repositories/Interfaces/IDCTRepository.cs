using System.Collections.Generic;
using System.Threading.Tasks;
using DreamsComeTrueAPI.Models;

namespace DreamsComeTrueAPI.Repositories.Interfaces
{
    public interface IDCTRepository
    {
         void Add<T>(T entity) where T : class;
         void Delete<T>(T entity) where T : class;
         Task<bool> SaveAll();
         Task<IEnumerable<User>> GetUsers();
         Task<User> GetUser(int id);
         Task<bool> AreConnected(string login, string comparedWith);
    }
}