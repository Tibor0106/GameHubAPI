namespace GameHubAPI.DTO.Game
{
    public class EditGameDTO
    {

        public int categoryId { get; set; }
        public string name { get; set; }
        public string linkId { get; set; }
        public string shortdescr { get; set; }
        public string longdescr { get; set; }

        public string icon { get; set; }
        public string banner { get; set; }
        public int addpopularity { get; set; }
    }
}
