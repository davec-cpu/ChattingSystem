using System.Collections.Concurrent;

namespace ChattingSystem.DataService
{
    public class TempDb
    {
        private readonly ConcurrentDictionary<string, string> _connection = new ConcurrentDictionary<string, string>();
        public List<string> Connectionlist { get; set; }
        //key: userId, value: connectionId
        public ConcurrentDictionary<string, string> connectedUserId { get; set; } = new ConcurrentDictionary<string, string>();
        public ConcurrentDictionary<int, int> userMessage { get; set; } = new ConcurrentDictionary<int, int>();
        public ConcurrentDictionary<string, string> connection => _connection;

    }
}
