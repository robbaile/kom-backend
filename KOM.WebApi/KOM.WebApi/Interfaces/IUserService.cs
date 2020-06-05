using KOM.Entities;
using KOM.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KOM.WebApi.Interfaces
{
    public interface IUserService
    {
        User Authenticate(string username, string password);

        User Register(RegisterModel registerModel);

        IEnumerable<User> GetAllUsers();

        User GetUserById(int id);

        List<Ride> GetRidesByUserId(int id);

        Ride AddRideByUserId(AddRideModel addRideModel);
    }
}
