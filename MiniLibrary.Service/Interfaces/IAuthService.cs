using MiniLibrary.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MiniLibrary.Service.Interfaces
{
    public interface IAuthService
    {
        Task<User> GetUserByEmailAsync(string email);

        Task<User> GetUserAsync(string id);

        Task<User> RegisterUserAsync(User user);
    }
}
