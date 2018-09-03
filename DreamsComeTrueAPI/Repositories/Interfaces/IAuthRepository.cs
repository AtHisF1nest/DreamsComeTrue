using System.Threading.Tasks;
using DreamsComeTrueAPI.Models;

namespace DreamsComeTrueAPI.Repositories.Interfaces
{
    public interface IAuthRepository
    {
         Task<User> Register(User user, string password);
         Task<User> Login(string login, string password);
         Task<bool> UserExists(string login);
    }
}