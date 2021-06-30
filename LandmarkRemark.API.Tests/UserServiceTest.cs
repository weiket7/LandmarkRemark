using LandmarkRemark.API.Database.Entities;
using LandmarkRemark.API.Database.Repositories;
using LandmarkRemark.API.Services;
using NSubstitute;
using NUnit.Framework;

namespace LandmarkRemark.API.Tests
{
    public class UserServiceTest
    {
        private IUserService _userService;

        [SetUp]
        public void SetUp()
        {
            IUserRepository userRepository = Substitute.For<IUserRepository>();
            userRepository.Get("Alan").Returns(new User { UserId=1, Username="Alan"});
            _userService = new UserService(userRepository);
        }

        [Test]
        public void GetOrCreateUser_WhenUserExist_WillGetUser()
        {
            IUserRepository userRepository = Substitute.For<IUserRepository>();
            userRepository.Get("Alan").Returns(new User { UserId = 1, Username = "Alan" });
            _userService = new UserService(userRepository);

            var user = _userService.GetOrCreate("Alan");
            Assert.AreEqual("Alan", user.Username);
        }

        [Test]
        public void GetOrCreateUser_WhenUserDoesNotExist_WillCreateUserAndReturn()
        {
            IUserRepository userRepository = Substitute.For<IUserRepository>();
            userRepository.Create(Arg.Any<User>()).Returns(new User { Username = "Charlie"});
            _userService = new UserService(userRepository);

            var user = _userService.GetOrCreate("Charlie");
            userRepository.Received(1).Create(Arg.Is<User>(x => x.Username == "Charlie"));
            Assert.AreEqual("Charlie", user.Username);
        }
    }
}