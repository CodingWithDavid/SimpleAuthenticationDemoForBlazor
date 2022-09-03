
//locals
using SimpleAuth.Data;

//3rd party
using Blazored.SessionStorage;

namespace SimpleAuth.Services
{
    public class AuthService
    {
        private List<User> validUsers = new List<User>(); 
        
        public AuthService()
        {
            User user = new()
            {
                FirstName = "Demo",
                EmailAddress = "demo@demo.com",
                Password = "Password1"
            };
            validUsers.Add(user);

            user = new()
            {
                FirstName = "Admin",
                EmailAddress = "admin@demo.com",
                Password = "Password1"
            };
            validUsers.Add(user);
        }

        public bool AuthenicateUser(User user, CustomAuthenicationStateProvider authProvider, ISessionStorageService storageService)
        {
            if (string.IsNullOrEmpty(user.EmailAddress) || string.IsNullOrEmpty(user.Password))
            {
                return false;
            }

            //lookup user and password
            var validUser = validUsers.Where(x => x.EmailAddress == user.EmailAddress && x.Password == user.Password).FirstOrDefault();

            if (validUser == null)
            {
                return false;
            }

            //Add Session Token
            validUser.AccessToken = DateTime.Now.ToString();
            authProvider.MarkUserAsAuthenicated(validUser.FirstName);
            storageService.SetItemAsStringAsync("userSession", validUser.EmailAddress);
            return true;
        }

        public User GetUser(string email)
        {
            User result = new ();
            result = validUsers.FirstOrDefault(x => x.EmailAddress == email)!;
            if (result == null)
                result = new();
            return result;
        }
    }
}
