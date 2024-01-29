namespace SteamV2Webapi.DTO
{
    public class MessageSendDTO
    {
        public string Message { get; set; }
        public int MessageId { get; set; }
        public int senderId { get; set; }
        public int reciverId { get; set; }

    }
}
