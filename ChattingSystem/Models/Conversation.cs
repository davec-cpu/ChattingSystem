using System.Text.Json;
using System.Text.Json.Serialization;
using ChattingSystem.Models.Expansions;

namespace ChattingSystem.Models
{
    public class Conversation
    {
        public int? Id { get; set; }
        public int? SiteId { get; set; }
        public string? Title { get; set; }
        public string? Thumbnail { get; set; }
        public short? Type { get; set; }
        public string? Feature { get; set; }
        public string? Taxonomy { get; set; }
        public string? Settings { get; set; }
        public short? Status { get; set; }
        [JsonIgnore]
        public int? TotalRecords { get; set; }

        public static Conversation From(ConversationExpansion.General general)
        {
            return new Conversation
            {
                Id = general.Id,
                SiteId = general.SiteId,
                Title = general.Title,
                Thumbnail = general.Thumbnail,
                Type = general.Type,
                Feature = general.Feature,
                Taxonomy = general.Taxonomy,
                Settings = general.Settings,
                Status = general.Status,
                TotalRecords = general.TotalRecords
            };
        }
       public override string ToString() => JsonSerializer.Serialize(this);
    }
}
