using Microsoft.EntityFrameworkCore;
using SteamV2Webapi.Objects;


namespace PTHUWEBAPI.Database
{
    public class AppDbContext : DbContext
    {
      public DbSet<User> users { get; set; }
      public DbSet<GameStats> game_stats { get; set; }
      public DbSet<Friend> friends { get; set; }
      public DbSet<Library> library { get; set; }
      public DbSet<Message> messages { get; set; }

       public AppDbContext(DbContextOptions options) : base(options)
     {
     }
       
    }
}
