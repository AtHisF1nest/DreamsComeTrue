using System.Threading.Tasks;
using DreamsComeTrueAPI.Models;
using System.Collections.Generic;
using DreamsComeTrueAPI.Models.Enums;

namespace DreamsComeTrueAPI.Repositories.Interfaces
{
    public interface ITodoRepository
    {
         Task<IEnumerable<TodoItem>> GetTodoItems(CategoryType type = CategoryType.NaDzis);
         Task<TodoItem> GetTodoItem(int id, CategoryType type = CategoryType.NaDzis);
         Task<IEnumerable<Category>> GetCategories(CategoryType type = CategoryType.NaDzis);
         Task<Category> AddCategory(Category category, CategoryType type = CategoryType.NaDzis);
         Task<bool> DeleteCategory(int id);
         Task<TodoItem> AddTodoItem(TodoItem todoItem, CategoryType type = CategoryType.NaDzis);
         Task<bool> DeleteTodoItem(int id);
    }
}