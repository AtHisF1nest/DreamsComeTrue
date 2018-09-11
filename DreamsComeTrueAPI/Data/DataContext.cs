using DreamsComeTrueAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DreamsComeTrueAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryTodoItemBinding> CategoryTodoItemBindings { get; set; }
        public DbSet<UserConnection> UserConnections { get; set; }
    }
}