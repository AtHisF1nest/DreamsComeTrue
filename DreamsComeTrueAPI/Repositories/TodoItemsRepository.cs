using System;
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
            return await _context.TodoItems.Include(x => x.Author).Include(x => x.Author.Photo).Include(x => x.UsersPair)
                .Where(x => x.UsersPair.RelationshipType == RelationshipType.SeriousRelationship 
                        && (x.UsersPair.User.Login == _actualUserLogin || x.UsersPair.User2.Login == _actualUserLogin)).ToListAsync();
        }

        public async Task<TodoItem> GetTodoItem(int id)
        {
            return await _context.TodoItems.Include(x => x.Author).Include(x => x.Author.Photo).Include(x => x.UsersPair)
                .Include(x => x.UsersPair.User).Include(x => x.UsersPair.User2).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories.Include(x => x.Author).Include(x => x.UsersPair)
                .Where(x => x.UsersPair.RelationshipType == RelationshipType.SeriousRelationship 
                        && (x.UsersPair.User.Login == _actualUserLogin || x.UsersPair.User2.Login == _actualUserLogin)).ToListAsync();
        }

        public async Task<TodoItem> AddTodoItem(TodoItem todoItem)
        {
            todoItem.Created = DateTime.Now;
            var actualUser = await _context.Users.FirstOrDefaultAsync(x => x.Login == _actualUserLogin);
            var actualPair = await _context.UsersPairs.FirstOrDefaultAsync(x => x.User == actualUser || x.User2 == actualUser);
            todoItem.Author = actualUser;
            todoItem.UsersPairId = actualPair.Id;

            await _context.TodoItems.AddAsync(todoItem);
            if(await _context.SaveChangesAsync() == 0)
                return null;
            
            return todoItem;
        }

        public async Task<bool> DeleteTodoItem(int id)
        {
            _context.CategoryTodoItemBindings.RemoveRange(await _context.CategoryTodoItemBindings.Where(x => x.TodoItem.Id == id).ToListAsync());
            _context.TodoItems.Remove(await _context.TodoItems.FirstOrDefaultAsync(x => x.Id == id));
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Category> AddCategory(Category category)
        {
            var actualUser = await _context.Users.FirstOrDefaultAsync(x => x.Login == _actualUserLogin);
            var actualPair = await _context.UsersPairs.FirstOrDefaultAsync(x => x.User == actualUser || x.User2 == actualUser);
            category.Author = actualUser;
            category.UsersPairId = actualPair.Id;

            await _context.Categories.AddAsync(category);
            if(await _context.SaveChangesAsync() > 0)
                return category;
            else
                return null;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            _context.CategoryTodoItemBindings.RemoveRange(await _context.CategoryTodoItemBindings.Where(x => x.Category.Id == id).ToListAsync());
            _context.Categories.Remove(await _context.Categories.FirstOrDefaultAsync(x => x.Id == id));
            return await _context.SaveChangesAsync() > 0;
        }
    }
}