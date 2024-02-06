namespace GameHubAPI.Objects
{
    public class Category
    {
        public int Id {  get; set; }
        public string categoryName { get; set; }
        public int popularity { get; set; }
        public Category(int Id, string categoryName, int popularity)
        {
            this.Id = Id;
            this.categoryName = categoryName;
            this.popularity = popularity;
        }
    }
}
