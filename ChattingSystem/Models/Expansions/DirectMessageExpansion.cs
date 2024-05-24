using ChattingSystem.Models.Objects;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace ChattingSystem.Models.Expansions
{
    public class DirectMessageExpansion : DirectMessage
    {
        [DataObject]
        public class General : DirectMessage
        {
            public General() { }
            public General(DirectMessage directMessage)
            {
                Id = directMessage.Id;
                SenderId = directMessage.SenderId;
                ReceiverId = directMessage.ReceiverId;
                Content = directMessage.Content;
                CreatedAt = directMessage.CreatedAt;
                
            }

            [JsonPropertyName("sender")]
            public User? Sender { get; set; }
            [JsonPropertyName("receiver")]
            public User? Receiver { get; set; }

        }
    }
}
