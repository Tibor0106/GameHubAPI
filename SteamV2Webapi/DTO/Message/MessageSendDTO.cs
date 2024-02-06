namespace GameHubAPI.DTO.Message
{
    public class MessageSendDTO
    {
        public string Message { get; set; }
        public int senderId { get; set; }
        public int receiverId { get; set; }

    }
}
