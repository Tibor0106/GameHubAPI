using Microsoft.EntityFrameworkCore;
using Steam2WebApi.Objects;
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
      public DbSet<Cart> cart { get; set; }
      public DbSet<Category> category { get; set; }
     public DbSet<Publisher> publisher { get; set; }
      public DbSet<Shop> shop{ get; set; }
      public DbSet<Transaction> transactions { get; set; }

      public AppDbContext(DbContextOptions options) : base(options)
      {
      }
       
    }
}
