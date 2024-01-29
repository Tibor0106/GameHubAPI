using System.Reflection.Metadata;

namespace SteamV2Webapi.DTO
{
    public class UserDTO
    {
        public int userId { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string avatar { get; set; }
        public DateTime create_at { get; set; }
        public DateTime last_heartbeat { get; set;}
        public bool online { get; set; }
    }
}
