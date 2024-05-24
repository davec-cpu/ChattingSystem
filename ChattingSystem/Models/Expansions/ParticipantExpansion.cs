using ChattingSystem.Models.Objects;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text.Json;

namespace ChattingSystem.Models.Expansions
{
    public class ParticipantExpansion
    {
        public class General : Participant
        {
            public General(Participant participant)
            {
                Id = participant.Id;
                SiteId = participant.SiteId;
                UserId = participant.UserId;
                ConversationId = participant.ConversationId;
                Title = participant.Title;
                Type = participant.Type;
                Taxonomy = participant.Taxonomy;
                Settings = participant.Settings;
                Status = participant.Status;
                TotalRecords = participant.TotalRecords;
            }

            public string? Avatar { get; set; }
            private string? _settingsJson;
            public new string? Settings
            {
                get => _settingsJson;
                set
                {
                    _settingsJson = value;
                    if (!string.IsNullOrEmpty(_settingsJson))
                    {
                        Settings_ = JsonSerializer.Deserialize<ParticipantObject.Settings>(_settingsJson);
                    }
                }
            }

            private ParticipantObject.Settings? _settingsObject;
            public ParticipantObject.Settings? Settings_
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

            public User? User { get; set; }
            public Conversation? Conversation { get; set; }

        }
    }
}
