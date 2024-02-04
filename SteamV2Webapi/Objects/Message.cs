namespace SteamV2Webapi.Objects
{
    public class Message
    {
        public int Id { get; set; }
        public int senderId { get; set; }
        public int receiverId { get; set; }
        public string messageBody { get; set; }
        public DateTime messageSent { get; set; }
        public bool edited { get; set; }

        public Message(int senderId, int receiverId, string messageBody, DateTime messageSent, bool edited)
        {
        
            this.senderId = senderId;
            this.receiverId = receiverId;
            this.messageBody = messageBody;
            this.messageSent = messageSent;
            this.edited = edited;
        }
    }
}
