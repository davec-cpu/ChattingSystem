using System.ComponentModel;
using System.Text.Json.Serialization;

namespace ChattingSystem.Models.Objects
{
    public class MessageObject
    {
        public class Settings
        {
            [JsonPropertyName("pinned")]
            public bool? Pinned { get; set; }
        }
    }
}
