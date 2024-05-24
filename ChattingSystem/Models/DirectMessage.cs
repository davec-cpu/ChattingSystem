namespace ChattingSystem.Models
{
    public class DirectMessage
    {
        public int Id { get; set; }
        public int? SenderId { get; set; }
        public int? ReceiverId { get; set; }
        public string? Content { get; set; }
        public string? CreatedAt { get; set; }
    }
}
