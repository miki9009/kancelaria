using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kancelaria.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Kancelaria.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;
        }

//LOGIN
        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x=>x.UserName == username);
            if(user == null)
                return null;

            if(!VerifiedPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        private bool VerifiedPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i =0; i < computedHash.Length; i++)
                {
                    if((computedHash[i] != passwordHash[i])) return false;
                }
            }

            return true;
        }

//REGISTER
        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }

//USER EXISTS
        public async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x=> x.UserName == username);
        }

        public async Task<User> GetUser(int i)
        {
            return await _context.Users.SingleOrDefaultAsync(x=>x.Id == i);
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
    }
}