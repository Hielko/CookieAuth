using CookieAuth.Models;

namespace CookieAuth.Repo
{
    public interface IUsersRepo
    {
        UserToLogin? FindUser(string username, string password);
    }
}