﻿namespace SteamV2Webapi.Objects
{
    public class Friend
    {
        public int userId { get; set; }
        public int friendId { get; set; }
        public DateTime friend_since { get; set; }

        public Friend(int userId, int friendId, DateTime friend_since)
        {
            this.userId = userId;
            this.friendId = friendId;
            this.friend_since = friend_since;
        }   
    }
}