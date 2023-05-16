using FirstWebApp.Models;

namespace FirstWebApp.Infrastructure
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        Role GetRole(int userId); 
    }
    public class UserService : IUserService
    {
        UsersDbContext _db;
        public UserService(UsersDbContext db) => _db = db;
        public User Authenticate(string username, string password)
        {
            var user=_db.Users.FirstOrDefault(x=>x.Username == username && x.Password==password);
            return user!;
        }

        public Role GetRole(int userId)
        {
            var userRoles = _db.UserRoles.FirstOrDefault(x=>x.UserId == userId);
            if (userRoles != null)
            {
                var role = _db.Roles.FirstOrDefault(c => c.RoleId == userRoles.RoleId);
                return role!;
            }
            else
                return null!;
        }
    }
}
