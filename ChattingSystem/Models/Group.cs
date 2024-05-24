using System.Text.Json.Serialization;

namespace ChattingSystem.Models
{
    public class Group
    {
        public int Id { get; set; }
        public long? SiteId { get; set; }
        public long? ParentId { get; set; }
        public string? Title { get; set; }
        public string? Thumbnail { get; set; }
        public string? Description { get; set; }
        public string? Settings { get; set; }
        public long? Order { get; set; }
        public short? Status { get; set; }
        [JsonIgnore]
        public long? TotalRecords { get; set; }
    }
}
