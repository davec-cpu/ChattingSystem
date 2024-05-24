using System.Text.Json.Serialization;

namespace ChattingSystem.Models
{
    public class Participant
    {
        public int? Id { get; set; }
        public int? SiteId { get; set; }
        public int? UserId { get; set; }
        public int? ConversationId { get; set; }
        public string? Title { get; set; }
        public short? Type { get; set; }
        public string? Taxonomy { get; set; }
        public string? Settings { get; set; }
        public short? Status { get; set; }
        [JsonIgnore]
        public int? TotalRecords { get; set; }
    }
}
