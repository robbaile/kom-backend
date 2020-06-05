using System.Collections.Generic;
using System.Linq;

namespace KOM.Entities
{
    public class DbInitialiser
    {
        public static void Initialise(KOMContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;
            }

            var users = new User[]
            {
                 new User
                {
                    FirstName = "Rob",
                    LastName = "Baile",
                    Username = "robbaile8",
                    Password = "yehboy"
                },
                new User
                {
                    FirstName = "Ben",
                    LastName = "Baile",
                    Username = "benbaile10",
                    Password = "yehboy1"
                },
                new User
                {
                    FirstName = "Amelia",
                    LastName = "Fraser",
                    Username = "ameliafraser8",
                    Password = "yehboy2"
                }
            };

            var rides = new List<Ride>
            {
                new Ride
                {
                    User = users[0],
                    Distance = 21,
                    Time = 65
                },
                new Ride
                {
                    User = users[0],
                    Distance = 14,
                    Time = 47
                },
                new Ride
                {
                    User = users[0],
                    Distance = 32,
                    Time = 113
                },

            };


            foreach (var user in users)
            {
                context.Users.Add(user);
            }

            foreach (var ride in rides)
            {
                context.Rides.Add(ride);
            }

            context.SaveChanges();
        }
    }
}
