namespace GameHubAPI.Objects
{
    public class User
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string avatar { get; set; }
        public DateTime created_at { get; set; }
        public DateTime last_heartbeat { get; set; }
        public bool online { get; set; }

        public User(int id, string name, string username, string email, string password, string avatar, DateTime created_at, DateTime last_heartbeat, bool online)
        {
            Id = id;
            this.name = name;
            this.username = username;
            this.email = email;
            this.password = password;
            this.avatar = avatar;
            this.created_at = created_at;
            this.last_heartbeat = last_heartbeat;
            this.online = online;
        }
    }

}
