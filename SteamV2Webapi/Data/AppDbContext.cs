using Microsoft.EntityFrameworkCore;
using SteamV2Webapi.DTO;
using System.Runtime.InteropServices;


namespace PTHUWEBAPI.Database
{
    public class AppDbContext : DbContext
    {
      public DbSet<UserDTO> users { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
       
    }
}
