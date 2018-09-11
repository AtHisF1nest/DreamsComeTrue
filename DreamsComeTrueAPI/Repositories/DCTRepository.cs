using System.Collections.Generic;
using System.Threading.Tasks;
using DreamsComeTrueAPI.Data;
using DreamsComeTrueAPI.Models;
using DreamsComeTrueAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DreamsComeTrueAPI.Repositories
{
    public class DCTRepository : IDCTRepository
    {
        private readonly DataContext _context;
        public DCTRepository(DataContext context)
        {
            _context = context;

        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public async Task<bool> AreConnected(string login, string comparedWith)
        {
            return await _context.UserConnections.Include(x => x.UserA).Include(x => x.UserB)
                .AnyAsync(x => (x.UserA.Login == login && x.UserB.Login == comparedWith) || (x.UserB.Login == login && x.UserA.Login == comparedWith));
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.Include(x => x.Photo).FirstOrDefaultAsync(x => x.Id == id);
            
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.Include(x => x.Photo).ToListAsync();

            return users;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}