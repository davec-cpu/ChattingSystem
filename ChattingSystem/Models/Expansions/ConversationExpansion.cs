using ChattingSystem.Models.Objects;
using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace ChattingSystem.Models.Expansions
{
    public class ConversationExpansion
    {
        public class General : Conversation
        {
            public General() { }
            public General(Conversation conversation)
            {
                Id = conversation.Id;
                SiteId = conversation.SiteId;
                Title = conversation.Title;
                Thumbnail = conversation.Thumbnail;
                Type = conversation.Type;
                Feature = conversation.Feature;
                Taxonomy = conversation.Taxonomy;
                Settings = conversation.Settings;
                Status = conversation.Status;
                TotalRecords = conversation.TotalRecords;
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
            public IEnumerable<Group>? Groups { get; set; }
            public IEnumerable<Participant>? Participants { get; set; }
            public override string ToString() => JsonSerializer.Serialize(this);
        }
    }
}
