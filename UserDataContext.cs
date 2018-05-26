
using ProductApi.Models;

namespace ProductApi
{
    public class UserDataContext : IUserDataContext
    {
        public User CurrentUser { get; private set; }

        public void SetCurrentUser(User user)
        {
            CurrentUser = user;
        }
    }
}