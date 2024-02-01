using System.Reflection.Metadata;

namespace SteamV2Webapi.Objects
{
    public class Publisher
    {
        public int Id { get; set; }
        public int publisherId {  get; set; }
        public string publisherName { get; set; }
        public Blob publisherIcon { get; set; }
        public Publisher(int Id, int publisherId, string publisherName, Blob publisherIcon)
        {
            this.Id = Id;
            this.publisherId = publisherId;
            this.publisherName = publisherName;
            this.publisherIcon = publisherIcon;
        }
    }
}
