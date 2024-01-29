using MiniLibrary.Entity;
using MiniLibrary.Service.Interfaces;
using RestSharp;

namespace MiniLibrary.Service
{
    public class AuthService : ApiBaseService, IAuthService
    {
        private const string PATH = "Auth";

        public async Task<User> GetUserAsync(string id)
        {
            return await GetAsync<User>($"{PATH}/GetUser/{id}");
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await GetAsync<User>($"{PATH}/GetUserByEmail/{email}");
        }

        public async Task<User> RegisterUserAsync(User user)
        {
            return await PostAsync<User>($"{PATH}/RegisterUser", new { user.Email, user.PasswordHash });
        }
    }
}