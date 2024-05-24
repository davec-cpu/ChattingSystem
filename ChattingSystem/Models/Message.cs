 using System.Text.Json;
using System.Text.Json.Serialization;
using ChattingSystem.Models.Expansions;

namespace ChattingSystem.Models
{
    public class Message
    {
        public int? Id { get; set; }
        public int? SiteId { get; set; }
        public int? ParticipantId { get; set; }
        public int? ConversationId { get; set; }
        public string? Content { get; set; }
        public string? Settings { get; set; }
        public short? Status { get; set; }
        [JsonIgnore]
        public int? TotalRecords { get; set; }

        public Message(){}
        public static Message From(MessageExpansion.General general)
        {
            return new Message
            {
                Id = general.Id,
                SiteId = general.SiteId,
                ParticipantId = general.ParticipantId,
                ConversationId = general.ConversationId,
                Content = general.Content,
                Status = general.Status,
                TotalRecords = general.TotalRecords
            };
        }

        JsonSerializerOptions options = new()
        {
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true,
        };

        public override string ToString() => JsonSerializer.Serialize(this, options);
    }
}
