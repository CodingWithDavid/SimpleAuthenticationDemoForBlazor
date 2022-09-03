
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

//3rd Party
using Blazored.SessionStorage;

namespace SimpleAuth.Services
{
    public class CustomAuthenicationStateProvider : AuthenticationStateProvider
    {
        public ISessionStorageService _localStorageService { get; }
        public IUserService userService { get; set; }
        //private readonly HttpClient _httpClient;

        public CustomAuthenicationStateProvider(ISessionStorageService sessionService, IUserService UserService)
        {
            _localStorageService = sessionService;
            userService = UserService;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsIdentity identity = new();

            var emailAddress = await _localStorageService.GetItemAsync<string>("userSession");

            if (emailAddress != null)
            {
                //make sure the user is logged in
                var inUser = userService.GetUserByAccessTokenAsync(emailAddress).Result;
                if (!string.IsNullOrEmpty(inUser.AccessToken))
                {
                    identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, inUser.FirstName),
                    }, "apiauth_type");
                }
            }
            else
            {
                identity = new ClaimsIdentity();
            }
            var user = new ClaimsPrincipal(identity);
            return await Task.FromResult(new AuthenticationState(user));
        }

        public void MarkUserAsAuthenicated(string userEmail)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, userEmail),
            }, "apiauth_type");

            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }
}
