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
    public class DCTRepository : IDCTRepository
    {
        private readonly DataContext _context;
        private readonly string _actualUserLogin;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DCTRepository(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;


            _actualUserLogin = _httpContextAccessor.HttpContext.User.Identity.Name;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<IEnumerable<ManagementType>> GetManagementTypes()
        {
            var types = await _context.ManagementTypes.ToListAsync();

            return types;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.Include(x => x.Photo).FirstOrDefaultAsync(x => x.Id == id);

            return user;
        }

        public async Task<IEnumerable<User>> GetUsers(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
                return await _context.Users.Include(x => x.Photo).ToListAsync();
            else
                return await _context.Users.Include(x => x.Photo)
                    .Where(x => (!string.IsNullOrEmpty(name) && x.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase)) || x.Login.Contains(name, StringComparison.InvariantCultureIgnoreCase)).ToListAsync();
        }

        public async Task<bool> InviteUser(int id)
        {
            var actualUser = await _context.Users.FirstOrDefaultAsync(x => x.Login == _actualUserLogin);
            var invitedUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            await _context.UserInvitations.AddAsync(new UserInvitation {
                Date = DateTime.Now,
                UserInvitating = actualUser,
                InvitedUser = invitedUser,
                InvitationType = InvitationType.Waiting
            });

            return await SaveAll();
        }

        public async Task<bool> IsInvited(int id)
        {
            var actualUser = await _context.Users.FirstOrDefaultAsync(x => x.Login == _actualUserLogin);
            var invitedUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            var invitation = await _context.UserInvitations
                .FirstOrDefaultAsync(x => x.UserInvitating == actualUser && x.InvitedUser == invitedUser && x.InvitationType == InvitationType.Waiting);

            return invitation != null;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UnInviteUser(int id)
        {
            var actualUser = await _context.Users.FirstOrDefaultAsync(x => x.Login == _actualUserLogin);
            var invitedUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            var invitation = await _context.UserInvitations.FirstOrDefaultAsync(x => x.UserInvitating == actualUser && x.InvitedUser == invitedUser);

            _context.UserInvitations.Remove(invitation);

            return await SaveAll();
        }
    }
}