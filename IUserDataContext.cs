
using ProductApi.Models;

namespace ProductApi
{
    public interface IUserDataContext
    {
        // readonly fields
        User CurrentUser { get; }

        // setter methods
        void SetCurrentUser(User user);
    }
}