using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DreamsComeTrueAPI.Data;
using DreamsComeTrueAPI.Models;
using DreamsComeTrueAPI.Models.Enums;
using DreamsComeTrueAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DreamsComeTrueAPI.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly DataContext _context;
        private readonly string _actualUserLogin;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TodoRepository(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;

            _actualUserLogin = _httpContextAccessor.HttpContext.User.Identity.Name;
        }

        public async Task<IEnumerable<TodoItem>> GetTodoItems()
        {
            return await _context.TodoItems.Include(x => x.Author).Include(x => x.Author.Photo)
                .Where(x => x.UsersPair.RelationshipType == RelationshipType.SeriousRelationship 
                        && (x.UsersPair.User.Login == _actualUserLogin || x.UsersPair.User2.Login == _actualUserLogin)).ToListAsync();
        }

        public async Task<TodoItem> GetTodoItem(int id)
        {
            return await _context.TodoItems.Include(x => x.Author).Include(x => x.Author.Photo).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories.Include(x => x.Author).Include(x => x.CategoryType)
                .Where(x => x.UsersPair.RelationshipType == RelationshipType.SeriousRelationship 
                        && (x.UsersPair.User.Login == _actualUserLogin || x.UsersPair.User2.Login == _actualUserLogin)).ToListAsync();
        }

    }
}