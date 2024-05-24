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

        public async Task<ConversationGroup> Create(ConversationGroup conversationGroup)
        {
            try
            {
                Console.WriteLine("create conversation group");
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
                    Console.WriteLine("success");
                    return result;
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public Task<ConversationGroup> GetByConversationIdAndGroupId(int conversationId, int groupId)
        {
            throw new NotImplementedException();
        }
        public async Task<ConversationGroup> GetConversationIdByGroupId(int groupId)
        {
            try
            {
                string query = "SELECT * FROM ConversationGroup WHERE GroupId = @groupId";
                
                using (var conn = _context.CreateConnection())
                {
                    var result = await conn.QueryFirstOrDefaultAsync<ConversationGroup>(query, new {groupId});
                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
