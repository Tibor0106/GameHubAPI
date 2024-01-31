namespace SteamV2Webapi.Objects
{
    public class Game
    {
        public int Id { get; set; }
        public int categoryId { get; set; }
        
        public string name { get; set; }
        public string linkId { get; set; }
        public string shortdescr { get; set; }
        public string longdescr { get; set; }

        public string icon { get; set; }
        public string banner { get; set; }

        public Game(int Id, int categoryId, string name, string linkId, string shortdescr, string longdescr, string icon, string banner)
        {
            this.Id = Id;
            this.categoryId = categoryId;
            this.name = name;
            this.linkId = linkId;
            this.shortdescr = shortdescr;
            this.longdescr = longdescr;
            this.icon = icon;
            this.banner = banner;
        }
    }
}
