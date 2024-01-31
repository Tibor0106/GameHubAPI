using Microsoft.EntityFrameworkCore;
using SteamV2Webapi.DTO;
using SteamV2Webapi.Objects;
using System.Runtime.InteropServices;


namespace PTHUWEBAPI.Database
{
    public class AppDbContext : DbContext
    {
      public DbSet<UserDTO> users { get; set; }
      public DbSet<GameStats> game_stats { get; set; }
      public DbSet<Friends> friends { get; set; }
        public DbSet<Library> library { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
     {
     }
       
    }
}
