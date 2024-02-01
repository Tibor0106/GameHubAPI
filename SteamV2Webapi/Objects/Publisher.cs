using System.Reflection.Metadata;

namespace SteamV2Webapi.Objects
{
    public class Publisher
    {
        public int Id { get; set; }
        public int publisherId {  get; set; }
        public string publisherName { get; set; }
        public string publisherIcon { get; set; } //kb 2,4 mrd karakter => mysql longtext kb 4mrd karakter
        public Publisher(int publisherId, string publisherName, string publisherIcon)
        {
            this.Id = Id;
            this.publisherId = publisherId;
            this.publisherName = publisherName;
            this.publisherIcon = publisherIcon;
        }
  
    }
}
