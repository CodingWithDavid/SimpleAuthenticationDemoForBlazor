using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace SimpleAuth.Services
{
    public class CustomAuthenicationStateProvider : AuthenticationStateProvider
    {
        public ISessionStorageService _localStorageService { get; }
        //public IUserService _userService { get; set; }
        //private readonly HttpClient _httpClient;

        public CustomAuthenicationStateProvider(ISessionStorageService sessionService)
        {
            _localStorageService = sessionService;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsIdentity identity = new();

            var emailAddress = await _localStorageService.GetItemAsync<string>("emailAddress");

            if (emailAddress != null)
            {
                identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, emailAddress), 
                }, "apiauth_type");
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

            //var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);

            //ClaimsIdentity identity;

            //if (accessToken != null && accessToken != string.Empty)
            //{
            //    User user = await _userService.GetUserByAccessTokenAsync(accessToken);
            //    identity = GetClaimsIdentity(user);
            //}
            //else
            //{
            //    identity = new ClaimsIdentity();
            //}

            //var claimsPrincipal = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }
}
