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

        public async Task<IEnumerable<TodoItem>> GetTodoItems(CategoryType type = CategoryType.NaDzis, List<int> categoryIds = null)
        {
            List<int> bindings = new List<int>();

            if(categoryIds != null && categoryIds.Count > 0)
            {
                var temp = await _context.CategoryTodoItemBindings.Include(x => x.Category).Include(x => x.TodoItem).Where(x => categoryIds.Contains(x.Category.Id)).ToListAsync();
                temp.ForEach(x => {
                    if(temp.Where(y => y.TodoItem.Id == x.TodoItem.Id).Count() == categoryIds.Count)
                        bindings.Add(x.TodoItem.Id);
                });
            }
            else
                categoryIds = null;

            return await _context.TodoItems.Include(x => x.Author).Include(x => x.Author.Photo).Include(x => x.UsersPair)
                .Where(x => x.CategoryType == type && x.UsersPair.RelationshipType == RelationshipType.SeriousRelationship 
                        && (x.UsersPair.User.Login == _actualUserLogin || x.UsersPair.User2.Login == _actualUserLogin)
                        && (categoryIds == null || bindings.Contains(x.Id))).ToListAsync();
        }

        public async Task<TodoItem> GetTodoItem(int id, CategoryType type = CategoryType.NaDzis)
        {
            return await _context.TodoItems.Include(x => x.Author).Include(x => x.Author.Photo).Include(x => x.UsersPair)
                .Include(x => x.UsersPair.User).Include(x => x.UsersPair.User2).FirstOrDefaultAsync(x => x.CategoryType == type && x.Id == id);
        }

        public async Task<IEnumerable<Category>> GetCategories(CategoryType type = CategoryType.NaDzis)
        {
            return await _context.Categories.Include(x => x.Author).Include(x => x.UsersPair)
                .Where(x => x.CategoryType == type && x.UsersPair.RelationshipType == RelationshipType.SeriousRelationship 
                        && (x.UsersPair.User.Login == _actualUserLogin || x.UsersPair.User2.Login == _actualUserLogin)).ToListAsync();
        }

        public async Task<TodoItem> AddTodoItem(TodoItem todoItem, CategoryType type = CategoryType.NaDzis)
        {
            todoItem.Created = DateTime.Now;
            var actualUser = await _context.Users.FirstOrDefaultAsync(x => x.Login == _actualUserLogin);
            var actualPair = await _context.UsersPairs.FirstOrDefaultAsync(x => x.User == actualUser || x.User2 == actualUser);
            todoItem.Author = actualUser;
            todoItem.UsersPairId = actualPair.Id;
            todoItem.CategoryType = type;

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

        public async Task<Category> AddCategory(Category category, CategoryType type = CategoryType.NaDzis)
        {
            var actualUser = await _context.Users.FirstOrDefaultAsync(x => x.Login == _actualUserLogin);
            var actualPair = await _context.UsersPairs.FirstOrDefaultAsync(x => x.User == actualUser || x.User2 == actualUser);
            category.Author = actualUser;
            category.UsersPairId = actualPair.Id;
            category.CategoryType = type;

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

        public async Task<IEnumerable<TodoItem>> GetConnectedTodoItems(int categoryId, CategoryType type = CategoryType.NaDzis)
        {
            var connections = await _context.CategoryTodoItemBindings.Include(x => x.TodoItem).Where(x => x.Category.Id == categoryId).ToListAsync();

            return await _context.TodoItems.Include(x => x.Author).Include(x => x.Author.Photo).Include(x => x.UsersPair)
                .Where(x => connections.Any(y => x.Id == y.TodoItem.Id) && x.CategoryType == type && x.UsersPair.RelationshipType == RelationshipType.SeriousRelationship 
                        && (x.UsersPair.User.Login == _actualUserLogin || x.UsersPair.User2.Login == _actualUserLogin)).ToListAsync();
        }

        public async Task<IEnumerable<TodoItem>> GetNotConnectedTodoItems(int categoryId, CategoryType type = CategoryType.NaDzis)
        {
            var connections = await _context.CategoryTodoItemBindings.Include(x => x.TodoItem).Where(x => x.Category.Id == categoryId).ToListAsync();

            return await _context.TodoItems.Include(x => x.Author).Include(x => x.Author.Photo).Include(x => x.UsersPair)
                .Where(x => !connections.Any(y => x.Id == y.TodoItem.Id) && x.CategoryType == type && x.UsersPair.RelationshipType == RelationshipType.SeriousRelationship 
                        && (x.UsersPair.User.Login == _actualUserLogin || x.UsersPair.User2.Login == _actualUserLogin)).ToListAsync();
        }

        public async Task<bool> ConnectItems(int categoryId, int itemId)
        {
            var todo = await _context.TodoItems.FirstOrDefaultAsync(x => x.Id == itemId);

            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);

            if(await _context.CategoryTodoItemBindings.AnyAsync(x => x.Category == category && x.TodoItem == todo))
                return false;

            await _context.CategoryTodoItemBindings.AddAsync(new CategoryTodoItemBinding() {
                Category = category,
                TodoItem = todo
            });

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UnConnectItems(int categoryId, int itemId)
        {
            var connection = await _context.CategoryTodoItemBindings.FirstOrDefaultAsync(x => x.TodoItem.Id == itemId && x.Category.Id == categoryId);

            _context.CategoryTodoItemBindings.Remove(connection);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<History>> GetHistoryOfTodo(int todoId, CategoryType type = CategoryType.NaDzis)
        {
            return await _context.Histories.Where(x => x.TodoItem.Id == todoId).ToListAsync();
        }

        public async Task<bool> RealizeTodo(int id, string date)
        {
            var todo = await _context.TodoItems.FirstOrDefaultAsync(x => x.Id == id);
            todo.LastDone = DateTime.Parse(date);
            await _context.Histories.AddAsync(new History {
                Done = (DateTime)todo.LastDone,
                TodoItem = todo
            });

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteHistory(int id)
        {
            var history = await _context.Histories.Include(x => x.TodoItem).FirstOrDefaultAsync(x => x.Id == id);
            var todoItem = await _context.TodoItems.FirstOrDefaultAsync(x => x.Id == history.TodoItem.Id);

            if(todoItem.LastDone == history.Done)
            {
                todoItem.LastDone = null;

                var previousHistory = await _context.Histories.Include(x => x.TodoItem).OrderByDescending(x => x.Done).FirstOrDefaultAsync(x => x.TodoItem.Id == todoItem.Id && x.Id != id);
                if(previousHistory != null)
                    todoItem.LastDone = previousHistory.Done;

            }

            _context.Histories.Remove(history);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}