namespace ChattingSystem.Models
{
    public class Participants
    {
        public long? Id { get; set; }
        public long? SiteId { get; set; }
        public long? UserId { get; set; }
        public long? ConversationId { get; set; }
        public string? Title { get; set; }
        public short? Type { get; set; }
        public string? Taxonomy { get; set; }
        public string? Settings { get; set; }
        public short? Status { get; set; }
        public long? TotalRecords { get; set; }
    }
}
