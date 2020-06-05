using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KOM.WebApi.Models
{
    public class AddRideModel
    {
        public string Distance { get; set; }

        public string Time { get; set; }

        public int UserId { get; set; }
    }
}
