using System.Collections.Generic;
using System.Threading.Tasks;
using Kancelaria.API.Models;

namespace Kancelaria.API.Data
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string username, string password);
        Task<bool> UserExists(string username);

        Task<User> GetUser(int i);

        Task<List<User>> GetUsers();
    }
}