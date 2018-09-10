using System.Threading.Tasks;
using DreamsComeTrueAPI.Models;
using System.Collections.Generic;

namespace DreamsComeTrueAPI.Repositories.Interfaces
{
    public interface ITodoRepository
    {
         Task<IEnumerable<TodoItem>> GetTodoItems();
         Task<IEnumerable<Category>> GetCategories();
    }
}