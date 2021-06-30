using LandmarkRemark.API.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LandmarkRemark.API.Database
{
    public class DatabaseContext : DbContext
    {
        private readonly string _username;
        private readonly string _password;

        public DatabaseContext(IConfiguration configuration)
        {
            _username = configuration.GetValue<string>("Database:Username");
            _password = configuration.GetValue<string>("Database:Password");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseNpgsql($@"Host=localhost;Database=LandmarkRemark;Username={_username}; Password={_password}");
    }
}
