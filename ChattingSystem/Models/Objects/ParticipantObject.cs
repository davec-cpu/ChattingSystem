using System.ComponentModel;
using System.Text.Json.Serialization;

namespace ChattingSystem.Models.Objects
{
    public class ParticipantObject
    {
        public class Settings
        {
            public bool? Notification { get; set; }
        }
    }
}
