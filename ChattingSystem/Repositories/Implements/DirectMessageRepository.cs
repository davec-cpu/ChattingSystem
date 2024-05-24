using ChattingSystem.Data;
using ChattingSystem.Models;
using ChattingSystem.Repositories.Interfaces;
using Dapper;
using Newtonsoft.Json;

namespace ChattingSystem.Repositories.Implements
{
    public class DirectMessageRepository : IDirectMessageRepository
    {
        private readonly DapperDbContext _context;

        public DirectMessageRepository(DapperDbContext context)
        {
            _context = context;
        }
        public async Task<DirectMessage> Create(DirectMessage message)
        {
            string query = "INSERT INTO DirectMessage (SenderId, ReceiverId, Content, CreatedAt)" +
                            "OUTPUT INSERTED.*" +
                            "VALUES (@SenderId, @ReceiverId, @Content, @CreatedAt)";
            var parameters = new DynamicParameters();
            parameters.Add("SenderId", message.SenderId, System.Data.DbType.Int32);
            parameters.Add("ReceiverId", message.ReceiverId, System.Data.DbType.Int32);
            parameters.Add("Content", message.Content, System.Data.DbType.String);
            parameters.Add("CreatedAt", message.CreatedAt, System.Data.DbType.String);
            using (var conn = _context.CreateConnection())
            {
                var result = await conn.QuerySingleAsync<DirectMessage>(query, parameters);
                return result;
            }
        }

        public async Task<IEnumerable<DirectMessage?>> GetAllMsgsBySenderIdAndReceiverId(int senderId, int receiverId) 
        {
            string query = " SELECT * FROM DirectMessage WHERE SenderId = @senderId AND ReceiverId = @receiverId " +
                            "OR SenderId = @receiverId AND ReceiverId = @senderId ";

            var parameters = new DynamicParameters();
            parameters.Add("@senderId", senderId, System.Data.DbType.Int32);
            parameters.Add("@receiverId", receiverId, System.Data.DbType.Int32);

            using (var conn = _context.CreateConnection())
            {
                var result = await conn.QueryAsync<DirectMessage>(query, parameters);
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
                return result;
            }

        }
    }
}
