using System.Threading.Tasks;
using DreamsComeTrueAPI.Models;
using System.Collections.Generic;

namespace DreamsComeTrueAPI.Repositories.Interfaces
{
    public interface ITodoItemsRepository
    {
         Task<IEnumerable<TodoItem>> GetTodoItems();
    }
}