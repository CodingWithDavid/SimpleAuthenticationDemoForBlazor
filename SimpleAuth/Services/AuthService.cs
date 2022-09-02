using SimpleAuth.Data;

namespace SimpleAuth.Services
{
    public class AuthService
    {
        private List<User> validUsers = new List<User>();   
        public AuthService()
        {
            User user = new()
            {
                EmailAddress = "demo@demo.com",
                Password = "Password1"
            };
            validUsers.Add(user);

            user = new()
            {
                EmailAddress = "admin@demo.com",
                Password = "Password1"
            };
            validUsers.Add(user);
        }

        public bool AuthenicateUser(User user)
        {
            if (string.IsNullOrEmpty(user.EmailAddress) || string.IsNullOrEmpty(user.Password))
            {
                return false;
            }

            //lookup user and password
            if(validUsers.Where(x => x.EmailAddress == user.EmailAddress && x.Password == user.Password).FirstOrDefault() == null)
            {
                return false;
            }
            return true;
        }
    }
}
