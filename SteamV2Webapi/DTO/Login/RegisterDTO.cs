using System.Reflection.Metadata;

namespace SteamV2Webapi.DTO.Login
{
    public class RegisterDTO
    {
        public string name { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public Blob avatar { get; set; }



    }
}
