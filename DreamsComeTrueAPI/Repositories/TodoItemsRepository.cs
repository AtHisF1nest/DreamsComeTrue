using System.Collections.Generic;
using System.Threading.Tasks;
using DreamsComeTrueAPI.Data;
using DreamsComeTrueAPI.Models;
using DreamsComeTrueAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DreamsComeTrueAPI.Repositories
{
    public class TodoItemsRepository : ITodoItemsRepository
    {
        private readonly DataContext _context;
        public TodoItemsRepository(DataContext context)
        {
            _context = context;

        }

        public async Task<IEnumerable<TodoItem>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }
    }
}