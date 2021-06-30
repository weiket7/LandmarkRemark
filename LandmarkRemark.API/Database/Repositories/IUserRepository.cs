using LandmarkRemark.API.Database.Entities;

namespace LandmarkRemark.API.Database.Repositories
{
    public interface IUserRepository
    {
        User Get(string username);
        User Create(User user);
    }
}