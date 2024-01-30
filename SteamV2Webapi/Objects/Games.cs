namespace SteamV2Webapi.Objects
{
    public class Games
    {
        public int gameId { get; set; }
        public int categoryId { get; set; }
        
        public string name { get; set; }
public string linkId { get { return name.replace(' ', '_').ToLower(); } set; }
        public string shortdescr { get; set; }
        public string longdescr { get; set; }

        public string icon { get; set; }
        public string banner { get; set; }
    }
}
