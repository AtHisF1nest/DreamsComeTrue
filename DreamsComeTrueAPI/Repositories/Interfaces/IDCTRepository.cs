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
         Task<User> GetCurrentUser();
         Task<IEnumerable<ManagementType>> GetManagementTypes();
         Task<bool> InviteUser(int id);
         Task<bool> UnInviteUser(int id);
         Task<bool> IsInvited(int id);
        Task<bool> Exists(string login);
        Task<Photo> UploadPhoto(string fileName, int userId);
        Task<bool> DeletePhoto(int id);
        Task<Photo> GetPhoto(int id);
        Task<IEnumerable<User>> GetUsersForInvite(string name);
        Task<bool> Inviting(int id);
        Task<bool> AcceptInvite(int id);
        Task<bool> HasPair(int userId);
        Task<bool> LeavePair();
    }
}