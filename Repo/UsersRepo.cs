using CookieAuth.Models;

namespace CookieAuth.Repo
{
    public class UsersRepo(IConfiguration config) : IUsersRepo
    {
        public List<UserToLogin>? users = config.GetSection("Users").Get<List<UserToLogin>>();

        public List<UserToLogin>? GetUsers()
        {
            return users;
        }

    }
}
