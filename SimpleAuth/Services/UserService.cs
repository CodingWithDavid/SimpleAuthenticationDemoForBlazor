using SimpleAuth.Data;

namespace SimpleAuth.Services
{
    public class UserService : IUserService
    {
        private readonly AuthService _authService;
        public UserService(AuthService authService)
        {
            _authService = authService;
        }

        public Task<User> GetUserByAccessTokenAsync(string accessToken)
        {
            User u = _authService.GetUser(accessToken);
            return Task.FromResult(u);
        }

        public Task<User> LoginAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> RegisterUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
