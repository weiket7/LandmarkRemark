using LandmarkRemark.API.Database.Entities;
using LandmarkRemark.API.Database.Repositories;

namespace LandmarkRemark.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetOrCreate(string username)
        {
            User user = Get(username);
            return user ?? Create(username);
        }

        private User Get(string username)
        {
            return _userRepository.Get(username);
        }

        private User Create(string username)
        {
            User user = new User
            {
                Username = username,
            };
            return _userRepository.Create(user);
        }
    }
}
