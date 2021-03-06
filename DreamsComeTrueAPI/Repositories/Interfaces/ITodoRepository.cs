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
         Task<TodoItem> GetTodoItem(int id);
         Task<IEnumerable<Category>> GetCategories(CategoryType type = CategoryType.NaDzis);
         Task<Category> AddCategory(Category category, CategoryType type = CategoryType.NaDzis);
         Task<bool> DeleteCategory(int id);
         Task<TodoItem> AddTodoItem(TodoItem todoItem, CategoryType type = CategoryType.NaDzis);
         Task<bool> DeleteTodoItem(int id);
         Task<bool> ConnectItems(int categoryId, int itemId);
         Task<bool> UnConnectItems(int categoryId, int itemId);
         Task<IEnumerable<History>> GetHistoryOfTodo(int todoId);
         Task<bool> RealizeTodo(int id, string date);
         Task<bool> DeleteHistory(int id);
         Task<IEnumerable<TodoItem>> GetDoneTodoItems();
         Task<IEnumerable<Event>> GetEvents();
        Task<int> AddEvent(Event eventItem);
        Task<bool> DeleteEvent(int eventId);
        Task<bool> UpdateEvent(Event eventItem);
    }
}