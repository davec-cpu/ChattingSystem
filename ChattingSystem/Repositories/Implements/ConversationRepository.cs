using ChattingSystem.Data;
using ChattingSystem.Models;
using ChattingSystem.Repositories.Interfaces;
using Dapper;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ChattingSystem.Repositories.Implements
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly DapperDbContext _context;
        public ConversationRepository(DapperDbContext context)
        {
            _context = context;
        }
        public async Task<Conversation>? GetById(int? Id)
        {
            string query = "SELECT * FROM Conversation WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                var conversation = await connection.QueryFirstOrDefaultAsync<Conversation>(query, new { Id});
                return conversation;
            }
        }

        public async Task<Conversation>? Create(Conversation? conversation)
        {
            Conversation result = new Conversation();
            try
            {
                string query = "INSERT INTO Conversation (SiteId, Title, Status) " +
                    "OUTPUT INSERTED.*" +
                    "values(@SiteId, @Title, @Status);";
                var parammeters = new DynamicParameters();
                parammeters.Add("SiteId", conversation.SiteId, System.Data.DbType.Int32);
                parammeters.Add("Title", conversation.Title, System.Data.DbType.String);
                parammeters.Add("Status", conversation.Status, System.Data.DbType.Int32);

                using (var connection = _context.CreateConnection())
                {
                    result = await connection.QuerySingleAsync<Conversation>(query, parammeters);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<(IEnumerable<Conversation?>, int?)> GetByUserId(int? userId)
        {
            string query = "SELECT Conversation.*, COUNT(Conversation.Id) OVER() AS \"TotalRecords\"  " +
            "FROM Conversation INNER JOIN Participant ON Conversation.Id = Participant.ConversationId WHERE Participant.UserId = @userId;";
            using (var connection = _context.CreateConnection())
            {
                var conversation = await connection.QueryFirstOrDefaultAsync<Conversation>(query, new { userId });
                var totalrecord = conversation.TotalRecords;
                var ieparticipant = new[] { conversation };
                return (ieparticipant, totalrecord);
            }
        }

        public async Task<Conversation>? Delete(int? conId)
        {
            string query = "DELETE FROM Conversation " +
                "OUTPUT DELETED.* " +
                "WHERE Conversation.Id = @conId";
            using (var con = _context.CreateConnection())
            {
                var result = await con.QueryFirstOrDefaultAsync<Conversation>(query, new { conId });
                return result;
            }
        }
    }
}
