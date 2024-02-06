namespace GameHubAPI.Objects
{
    public class FriendRequest
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public int friendId { get; set; }
        public FriendRequest(int Id, int userId, int friendId)
        {
            this.Id = Id;
            this.userId = userId;
            this.friendId = friendId;
        }
    }
}
