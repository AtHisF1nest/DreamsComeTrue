using System.Threading.Tasks;
using DreamsComeTrueAPI.Models;
using System.Collections.Generic;
using DreamsComeTrueAPI.Models.Enums;
using DreamsComeTrueAPI.Dtos;

namespace DreamsComeTrueAPI.Repositories.Interfaces
{
    public interface ITodoRepository
    {
         Task<IEnumerable<TodoItem>> GetTodoItems(CategoryType type = CategoryType.NaDzis, List<int> categoryIds = null);
         Task<IEnumerable<TodoItem>> GetConnectedTodoItems(int categoryId, CategoryType type = CategoryType.NaDzis);
         Task<IEnumerable<TodoItem>> GetNotConnectedTodoItems(int categoryId, CategoryType type = CategoryType.NaDzis);
         Task<TodoItem> GetTodoItem(int id, CategoryType type = CategoryType.NaDzis);
         Task<IEnumerable<Category>> GetCategories(CategoryType type = CategoryType.NaDzis);
         Task<Category> AddCategory(Category category, CategoryType type = CategoryType.NaDzis);
         Task<bool> DeleteCategory(int id);
         Task<TodoItem> AddTodoItem(TodoItem todoItem, CategoryType type = CategoryType.NaDzis);
         Task<bool> DeleteTodoItem(int id);
         Task<bool> ConnectItems(int categoryId, int itemId);
         Task<bool> UnConnectItems(int categoryId, int itemId);
         Task<IEnumerable<History>> GetHistoryOfTodo(int todoId, CategoryType type = CategoryType.NaDzis);
         Task<bool> RealizeTodo(int id, string date);
    }
}