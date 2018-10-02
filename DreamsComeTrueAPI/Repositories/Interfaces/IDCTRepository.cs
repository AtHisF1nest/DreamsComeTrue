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
         Task<IEnumerable<User>> GetUsers(string name);
         Task<User> GetUser(int id);
         Task<IEnumerable<ManagementType>> GetManagementTypes();
         Task<bool> InviteUser(int id);
         Task<bool> UnInviteUser(int id);
         Task<bool> IsInvited(int id);
    }
}