using ChattingSystem.Data;
using ChattingSystem.Models;
using ChattingSystem.Repositories.Interfaces;
using Dapper;

namespace ChattingSystem.Repositories.Implements
{
    public class GroupUserRepository : IGroupUserRepository
    {
        private readonly DapperDbContext _context;
        public GroupUserRepository(DapperDbContext context)
        {
            _context = context;
        }
        public async Task<GroupUser>? Create(GroupUser? groupUser)
        {
            string query = "INSERT INTO GroupUser(GroupId, UserId)" +
                            "OUTPUT INSERTED.*" +
                            "VALUES (@GroupId, @UserId)";
            var parameters = new DynamicParameters();
            parameters.Add("GroupId", groupUser.GroupId, System.Data.DbType.Int32);
            parameters.Add("UserId", groupUser.UserId, System.Data.DbType.Int32);

            using (var con = _context.CreateConnection())
            {
                var result = await con.QuerySingleAsync<GroupUser>(query, parameters);
                return result;
            }
        }

        public async Task<IEnumerable<GroupUser>>? DeleteByGroupId(int? groupId)
        {
            string query = "DELETE FROM GroupUser " +
                "OUTPUT DELETED.* " +
                "WHERE GroupUser.GroupId = @groupId";
            using (var con = _context.CreateConnection())
            {
                var result = await con.QueryAsync<GroupUser>(query, new { groupId });
                return result;
            }
        }

        public async Task<IEnumerable<GroupUser>>? GetByGroupId(int? groupId)
        {
            string query = "SELECT * FROM GroupUser WHERE GroupUser.GroupId = @groupId";
            
            using (var con = _context.CreateConnection())
            {
                var result = await con.QueryAsync<GroupUser>(query, new {groupId});
                return result;
            }
        }

        public async Task<IEnumerable<GroupUser>>? GetByUserId(int? userId)
        {
            string query = "SELECT * FROM GroupUser WHERE GroupUser.UserId = @userId";
            using (var con = _context.CreateConnection())
            {
                var result = await con.QueryAsync<GroupUser>(query, new { userId });
                return result;
            }
        }

    }
}
