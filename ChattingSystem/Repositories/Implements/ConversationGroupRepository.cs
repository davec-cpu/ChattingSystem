using ChattingSystem.Data;
using ChattingSystem.Models;
using ChattingSystem.Repositories.Interfaces;
using Dapper;

namespace ChattingSystem.Repositories.Implements
{
    public class ConversationGroupRepository : IConversationGroupRepository
    {
        private readonly DapperDbContext _context;
        public ConversationGroupRepository(DapperDbContext context)
        {
            _context = context;
        }

        public async Task<ConversationGroup>? Create(ConversationGroup? conversationGroup)
        {
            try
            {
                string query = "INSERT INTO ConversationGroup (SiteId, ConversationId, GroupId) " +
                    "OUTPUT INSERTED.*" +
                    "VALUES (@SiteId, @ConversationId, @GroupId);";
                var parameters = new DynamicParameters();
                parameters.Add("SiteId", conversationGroup.SiteId, System.Data.DbType.Int32);
                parameters.Add("ConversationId", conversationGroup.ConversationId, System.Data.DbType.Int32);
                parameters.Add("GroupId", conversationGroup.GroupId, System.Data.DbType.Int32);

                using (var connection = _context.CreateConnection())
                {
                    var result = await connection.QuerySingleAsync<ConversationGroup>(query, parameters);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ConversationGroup>? Delete(int? groupId)
        {
            string query = "DELETE FROM ConversationGroup " +
                "OUTPUT DELETED.* " +
                "WHERE ConversationGroup.GroupId = @groupId";
            using (var con = _context.CreateConnection())
            {
                var result = await con.QuerySingleAsync<ConversationGroup>(query, new { groupId });
                return result;
            }
        }

        public async Task<int>? GetConversationIdByGroupId(int? groupId)
        {
            try
            {
                string query = "SELECT ConversationId FROM ConversationGroup WHERE GroupId = @groupId";
                
                using (var conn = _context.CreateConnection())
                {
                    var result = await conn.QueryFirstOrDefaultAsync<int>(query, new {groupId});
                    return result;
                }
            }
            catch (Exception ex)
            {
    
                throw;
            }
        }
    }
}
