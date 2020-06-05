using System;
using System.Collections.Generic;
using System.Text;

namespace KOM.Entities
{
    public class Ride
    {
        public int Id { get; set; }
        public User User { get; set; }

        public int Distance { get; set; }

        public int Time { get; set; }
    }
}
