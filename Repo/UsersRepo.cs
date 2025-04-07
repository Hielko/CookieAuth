using CookieAuth.Models;

namespace CookieAuth.Repo
{
    public class UsersRepo(IConfiguration config) : IUsersRepo
    {
        public List<UserToLogin>? users = config.GetSection("Users").Get<List<UserToLogin>>();

        public UserToLogin? FindUser(string username, string password)
        {
            var user = users?.Find(c => c.UserName == username && c.Password == password);
            return user;
        }

    }
}
