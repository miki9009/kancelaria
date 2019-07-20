using System.Collections.Generic;
using System.Threading.Tasks;
using Kancelaria.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Kancelaria.API.Data
{
    public class kancelariaRepository : IKancelariaRepository
    {
        readonly DataContext _context;
        public kancelariaRepository(DataContext context)
        {
                _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
                _context.Remove(entity);
        }

        public Task<User> GetUser(int id)
        {
            var user = _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.Include(p=>p.Photos).ToListAsync();
        }


        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}