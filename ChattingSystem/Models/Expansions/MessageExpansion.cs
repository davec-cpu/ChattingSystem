using ChattingSystem.Models.Objects;
using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ChattingSystem.Models.Expansions
{
    public class MessageExpansion
    {

        [DataObject]
        public class General : Message
        {
            public General() { }
            public General(Message message)
            {
                Id = message.Id;
                SiteId = message.SiteId;
                ParticipantId = message.ParticipantId;
                ConversationId = message.ConversationId;
                Content = message.Content;
                Status = message.Status;
                TotalRecords = message.TotalRecords;
            }
            private string? _settingsJson;

            [JsonIgnore]
            public new string? Settings
            {
                get => _settingsJson;
                set
                {
                    _settingsJson = value;
                    if (!string.IsNullOrEmpty(_settingsJson))
                    {
                        Settings_ = JsonSerializer.Deserialize<MessageObject.Settings>(_settingsJson);
                    }
                }
            }

            private MessageObject.Settings? _settingsObject;

            [JsonPropertyName("settings")]
            public MessageObject.Settings? Settings_
            {
                get => _settingsObject;
                set
                {
                    _settingsObject = value;
                    if (_settingsObject != null)
                    {
                        _settingsJson = JsonSerializer.Serialize(_settingsObject);
                    }
                }
            }

            [JsonPropertyName("user")]
            public User? User { get; set; }
            [JsonPropertyName("participant")]
            public Participant? Participant { get; set; }
            [JsonPropertyName("conversation")]
            public Conversation? Conversation { get; set; }

            JsonSerializerOptions options = new()
            {
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true,
            };

            public override string ToString() => JsonSerializer.Serialize(this, options);
        }
    }
}
