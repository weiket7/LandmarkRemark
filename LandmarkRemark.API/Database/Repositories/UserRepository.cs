using System.Linq;
using LandmarkRemark.API.Database.Entities;

namespace LandmarkRemark.API.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _databaseContext;

        public UserRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public User Get(string username)
        {
            return _databaseContext.Users.FirstOrDefault(x => x.Username == username.Trim());
        }

        public User Create(User user)
        {
            _databaseContext.Add(user);
            _databaseContext.SaveChanges();
            return user;
        }
    }
}