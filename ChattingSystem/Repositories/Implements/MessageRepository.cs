using ChattingSystem.Data;
using ChattingSystem.Models;
using ChattingSystem.Repositories.Interfaces;
using Dapper;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace ChattingSystem.Repositories.Implements
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DapperDbContext _context;
        public MessageRepository(DapperDbContext context) { 
            _context = context;
        }
        public async Task<Message>? Create(Message? message)
        {
            string query = "INSERT INTO Message (SiteId, ParticipantId, ConversationId, Content, Status)" +
                "OUTPUT INSERTED.* " +
                "values( @SiteId, @ParticipantId, @ConversationId, @Content, @Status)";
            var parammeters = new DynamicParameters();
            parammeters.Add("SiteId", message.SiteId, System.Data.DbType.Int32);
            parammeters.Add("ParticipantId", message.ParticipantId, System.Data.DbType.Int32);
            parammeters.Add("ConversationId", message.ConversationId, System.Data.DbType.Int32);
            parammeters.Add("Content", message.Content, System.Data.DbType.String);
            parammeters.Add("Status", message.Status, System.Data.DbType.Int32);

            //Message result = new Message();
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QuerySingleAsync<Message>(query, parammeters);
                //Message msg = (Message)result;
                return result;
            }
        }

        public async Task<Message>? DeleteByConId(int? conId)
        {
            string query = "DELETE FROM Message " +
                "OUTPUT DELETED.* " +
                "WHERE Message.ConversationId = @conId";
            using (var con = _context.CreateConnection())
            {
                var result = await con.QueryFirstOrDefaultAsync<Message>(query, new { conId });
                return result;
            }
        }

        public async Task<IEnumerable<Message>>? GetByConversationId(int? conversationId)
        {
            string query = $"SELECT * FROM Message WHERE ConversationId{(conversationId == null ? "IS NULL" : "=@conversationId")}";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<Message>(query, new {conversationId});
                var ieresult = new[] { result };
                return result;
            }
        }

        
        public async Task<IEnumerable<Message>> DeleteByConversationIdAndParticipantId(int? conId, int? participantId)
        {
            string query = "DELETE FROM Message WHERE ConversationId = @conId AND ParticipantId = @participantId";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<Message>(query, new { conId, participantId });
                return result;
            }
        }
    }
}
