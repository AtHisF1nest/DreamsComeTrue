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

        public async Task<User> GetCurrentUser()
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Login == _actualUserLogin);
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.Include(x => x.Photo).FirstOrDefaultAsync(x => x.Id == id);

            return user;
        }

        public async Task<Photo> UploadPhoto(string fileName, int userId)
        {
            var photo = new Photo() {
                Url = fileName,
                UserId = userId
            };

            var existingPhoto = await _context.Photos.FirstOrDefaultAsync(x => x.UserId == userId);
            if (existingPhoto != null)
            {
                _context.Photos.Remove(existingPhoto);
                await _context.SaveChangesAsync();
            }
            
            await _context.Photos.AddAsync(photo);

            await _context.SaveChangesAsync();

            return photo;
        }

        public async Task<IEnumerable<User>> GetUsers(string name)
        {
            var actualUser = await _context.Users.FirstOrDefaultAsync(x => x.Login == _actualUserLogin);
            if (await _context.UsersPairs.AnyAsync(x => (x.User == actualUser && x.User2 != null) || (x.User2 == actualUser && x.User != null)))
                return new List<User>();

            if(string.IsNullOrWhiteSpace(name))
                return await _context.Users.Where(x => x.Login.ToLower() != _actualUserLogin).Include(x => x.Photo).ToListAsync();
            else
                return await _context.Users.Include(x => x.Photo)
                    .Where(x => !string.IsNullOrEmpty(name) && x.Login.ToLower() != _actualUserLogin.ToLower() && x.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase)).ToListAsync();
        }

        public async Task<bool> HasPair(int userId)
        {
            return await _context.UsersPairs.AnyAsync(x => (x.User.Id == userId && x.User2 != null) || (x.User2.Id == userId && x.User != null));
        }

        public async Task<IEnumerable<User>> GetUsersForInvite(string name)
        {
            var userPairs = await _context.UsersPairs.Include(x => x.User).Include(x => x.User2).Where(x => x.User == null || x.User2 == null).ToListAsync();
            List<int> availableIds = new List<int>(userPairs.Where(x => x.User != null).Select(x => x.User.Id));
            availableIds.AddRange(userPairs.Where(x => x.User2 != null).Select(x => x.User2.Id));

            return await _context.Users.Include(x => x.Photo)
                    .Where(x => (string.IsNullOrEmpty(name) || (!string.IsNullOrEmpty(name) && x.Login.Contains(name, StringComparison.InvariantCultureIgnoreCase)))
                                && availableIds.Contains(x.Id) && x.Login.ToLower() != _actualUserLogin)
                    .ToListAsync();
        }

        public async Task<bool> Exists(string login)
        {
            return await _context.Users.AnyAsync(x => x.Login.ToLower() == login.ToLower());
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

        public async Task<bool> AcceptInvite(int id)
        {
            var actualUser = await _context.Users.FirstOrDefaultAsync(x => x.Login == _actualUserLogin);
            var invitingUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            var invitation = await _context.UserInvitations.OrderByDescending(x => x.Date)
                .FirstOrDefaultAsync(x => 
                x.InvitedUser == actualUser && 
                x.UserInvitating == invitingUser && 
                x.InvitationType == InvitationType.Waiting);

            invitation.InvitationType = InvitationType.Accepted;
            var pairOfActual = await _context.UsersPairs.FirstOrDefaultAsync(x => x.User == actualUser || x.User2 == actualUser);
            if(pairOfActual.User2 == null)
                pairOfActual.User2 = invitingUser;
            else
                pairOfActual.User = invitingUser;
            
            var pairOfInviting = await _context.UsersPairs.FirstOrDefaultAsync(x => x.User == invitingUser || x.User2 == invitingUser);
            if(pairOfInviting.User2 == null)
                pairOfInviting.User2 = actualUser;
            else
                pairOfInviting.User = actualUser;

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

        public async Task<bool> Inviting(int id)
        {
            var actualUser = await _context.Users.FirstOrDefaultAsync(x => x.Login == _actualUserLogin);
            var invitingUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            var invitation = await _context.UserInvitations
                .FirstOrDefaultAsync(x => x.UserInvitating == invitingUser && x.InvitedUser == actualUser && x.InvitationType == InvitationType.Waiting);

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

        public async Task<bool> DeletePhoto(int id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(x => x.Id == id);
            _context.Remove(photo);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(x => x.Id == id);

            return photo;
        }

        public async Task<bool> LeavePair()
        {
            var actualUser = await GetCurrentUser();

            var actualPair = await _context.UsersPairs.Include(x => x.User2).FirstOrDefaultAsync(x => x.User == actualUser);
            actualPair.User2 = null;

            await SaveAll();

            var otherPair = await _context.UsersPairs.FirstOrDefaultAsync(x => x.User2 == actualUser);
            otherPair.User2 = null;

            return await SaveAll();
        }
    }
}