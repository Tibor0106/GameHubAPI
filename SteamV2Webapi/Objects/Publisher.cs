using System.Reflection.Metadata;

namespace SteamV2Webapi.Objects
{
    public class Publisher
    {
        public int publisherId {  get; set; }
        public string publisherName { get; set; }
        public Blob publisherIcon { get; set; }
        public Publisher(int publisherId, string publisherName, Blob publisherIcon)
        {
            this.publisherId = publisherId;
            this.publisherName = publisherName;
            this.publisherIcon = publisherIcon;
        }
    }
}
