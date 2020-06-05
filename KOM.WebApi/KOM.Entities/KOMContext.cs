using Microsoft.EntityFrameworkCore;

namespace KOM.Entities
{
    public class KOMContext : DbContext
    {
        public KOMContext(DbContextOptions<KOMContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Ride> Rides { get; set; }
    }
}
