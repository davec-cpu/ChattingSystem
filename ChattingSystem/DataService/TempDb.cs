using System.Collections.Concurrent;

namespace ChattingSystem.DataService
{
    public class TempDb
    {
        private readonly ConcurrentDictionary<string, string> _connection = new ConcurrentDictionary<string, string>();
        public List<string> Connectionlist { get; set; }
        public ConcurrentDictionary<string, string> activeUserId { get; set; } = new ConcurrentDictionary<string, string>();
        public ConcurrentDictionary<string, string> connection => _connection;

    }
}
