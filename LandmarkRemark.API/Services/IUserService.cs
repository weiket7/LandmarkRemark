using LandmarkRemark.API.Database.Entities;

namespace LandmarkRemark.API.Services
{
    public interface IUserService
    {
        User GetOrCreate(string username);
    }
}