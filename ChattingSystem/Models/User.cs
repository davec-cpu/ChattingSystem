using System.Text.Json;
using System.Text.Json.Serialization;

namespace ChattingSystem.Models
{
    public class User
    {
        public int? Id { get; set; }
        public int? SiteId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? ExternalId { get; set; }
        public string? ExternalLoginProvider { get; set; }
        public bool? EmailConfirmed { get; set; }
        public string? EmailToken { get; set; }
        public string? Password { get; set; }
        public string? Taxonomy { get; set; }
        public string? Feature { get; set; }
        public short? Status { get; set; }
        [JsonIgnore]
        public int? TotalRecords { get; set; }
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
