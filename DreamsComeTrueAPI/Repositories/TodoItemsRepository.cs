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
        private readonly DataContext context;
        public TodoItemsRepository(DataContext context)
        {
            this.context = context;

        }

        public async Task<IEnumerable<TodoItem>> GetTodoItems()
        {
            return await context.TodoItems.ToListAsync();
        }
    }
}