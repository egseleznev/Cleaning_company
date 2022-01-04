using LRSeleznev.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LRSeleznev.Data.Contexts
{
    public class SystemContext : DbContext
    {
        public DbSet<Utility> Utility { get; set; }
        public DbSet<UtilityPerSubscriber> UtilityIncludes { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public SystemContext(DbContextOptions<SystemContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
