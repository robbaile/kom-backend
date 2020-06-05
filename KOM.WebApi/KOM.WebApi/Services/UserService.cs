using KOM.Entities;
using KOM.WebApi.Helpers;
using KOM.WebApi.Interfaces;
using KOM.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KOM.WebApi.Services
{
    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test" }
        };

        private readonly AppSettings _appSettings;
        private readonly KOMContext _dbContext;

        public UserService(IOptions<AppSettings> appSettings, KOMContext dbContext)
        {
            _appSettings = appSettings.Value;
            _dbContext = dbContext;
        }

        public User Authenticate(string username, string password)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Username == username && x.Password == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user;
        }

        public User Register(RegisterModel registerModel)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Username == registerModel.Username);

            if (user != null)
            {
                return new User();
            }

            var newUser = _dbContext.Users.Add(new User
            {
                Username = registerModel.Username,
                Password = registerModel.Password
            }).Entity;

            _dbContext.SaveChanges();

            return newUser;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _dbContext.Users.ToList();
        }

        public User GetUserById(int id)
        {
            return _dbContext.Users
                .FirstOrDefault(x => x.Id == id);
        }

        public List<Ride> GetRidesByUserId(int userId)
        {
            return _dbContext.Rides
                .Include(x => x.User)
                .Where(x => x.User.Id == userId).ToList();
        }

        public Ride AddRideByUserId(AddRideModel addRideModel)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == addRideModel.UserId);

            if (user == null)
            {
                return new Ride();
            }

            var newRide = _dbContext.Rides.Add(new Ride
            {
                Time = Convert.ToInt32(addRideModel.Time),
                Distance = Convert.ToInt32(addRideModel.Distance),
                User = user
            }).Entity;

            _dbContext.SaveChanges();

            return newRide;
        }
    }
}
