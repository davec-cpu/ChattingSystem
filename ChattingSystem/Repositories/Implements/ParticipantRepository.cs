using ChattingSystem.Data;
using ChattingSystem.Models;
using ChattingSystem.Repositories.Interfaces;
using Dapper;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace ChattingSystem.Repositories.Implements
{
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly DapperDbContext _context;
        public ParticipantRepository(DapperDbContext context)
        {
            _context = context;
        }


        public async Task<Participant>? GetById(int? Id)
        {
            string query = "SELECT * FROM Participant WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                var participant = await connection.QueryFirstOrDefaultAsync<Participant>(query, new { Id });
                return participant;
            }
        }

        public async Task<IEnumerable<Participant>>? GetByConversationId(int? Id)
        {
            string query = "SELECT * FROM Participant WHERE ConversationId = @Id";
            using (var connection = _context.CreateConnection())
            {
                var participant = await connection.QueryFirstOrDefaultAsync<Participant>(query, new { Id });
                var ieparticipant = new[] { participant };
                return ieparticipant;
            }
        }

        public async Task<Participant>? GetByConversationIdObj(int? Id)
        {
            string query = "SELECT * FROM Participant WHERE ConversationId = @Id";
            using (var connection = _context.CreateConnection())
            {
                var participant = await connection.QueryFirstOrDefaultAsync<Participant>(query, new { Id });
                return participant;
            }
        }

        public async Task<Participant>? Create(Participant? participant)
        {
            try
            {
                string query = "INSERT INTO Participant (SiteId, UserId, ConversationId, Title, Status)" +
                "OUTPUT INSERTED.*" +
                " values(@SiteId, @UserId, @ConversationId, @Title, @Status)";
                var parammeters = new DynamicParameters();
                parammeters.Add("SiteId", participant.SiteId, System.Data.DbType.Int32);
                parammeters.Add("UserId", participant.UserId, System.Data.DbType.Int32);
                parammeters.Add("ConversationId", participant.ConversationId, System.Data.DbType.Int32);
                parammeters.Add("Title", participant.Title, System.Data.DbType.String);
                parammeters.Add("Status", participant.Status, System.Data.DbType.Int32);

                using (var connection = _context.CreateConnection())
                {
                    var result = await connection.QuerySingleAsync<Participant>(query, parammeters);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            
        }

        public async Task<Participant>? GetByConversationIdandUserId(int? ConversationId, int? UserId)
        {
            string query = "SELECT * FROM Participant WHERE ConversationId = @ConversationId AND UserId = @UserId";
            var parameters = new DynamicParameters();
            parameters.Add("@ConversationId", ConversationId, System.Data.DbType.Int32);
            parameters.Add("@UserId", UserId, System.Data.DbType.Int32);
            using (var connection = _context.CreateConnection())
            {
                var participant = await connection.QueryFirstOrDefaultAsync<Participant>(query, parameters);
                return participant;
            }
        }

        public async Task<IEnumerable<Participant>>? DeleteByConId(int? conId)
        {
            string query = "DELETE FROM Participant " +
                "OUTPUT DELETED.* " +
                "WHERE Participant.ConversationId = @conId";
            using (var con = _context.CreateConnection())
            {
                var result = await con.QueryAsync<Participant>(query, new { conId });
                return result;
            }
        }

        public async Task<Participant>? DeleteByConIdAndUserId(int? conId, int? userId)
        {
            string query = "DELETE FROM Participant " +
                     "OUTPUT DELETED.* " +
                     "WHERE ConversationId = @conId AND UserId = @userId";

            using (var conn = _context.CreateConnection())
            {
                var result = await conn.QueryFirstOrDefaultAsync<Participant>(query, new { conId, userId });
                return result;
            }
        }

        public async Task<int>? GetUserId(int? participantId)
        {
            string query = "SELECT UserId FROM Participant WHERE Id = @participantId";
            using (var conn = _context.CreateConnection())
            {
                var result = await conn.QueryFirstOrDefaultAsync<int>(query, new { participantId });
                return result;
            }
        }
    }
}
