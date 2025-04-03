using CookieAuth.Models;

namespace CookieAuth.Repo
{
    public interface IUsersRepo
    {
        List<UserToLogin>? GetUsers();
    }
}