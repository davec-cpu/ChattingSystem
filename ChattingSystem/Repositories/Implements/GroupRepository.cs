using ChattingSystem.Data;
using ChattingSystem.Models;
using ChattingSystem.Repositories.Interfaces;
using Dapper;

namespace ChattingSystem.Repositories.Implements
{
    public class GroupRepository : IGroupRepository
    {
        private readonly DapperDbContext _context;
        public GroupRepository(DapperDbContext context)
        {
            _context = context;
        }
        public async Task<Group> GetById(int? groupId)
        {
            string query = "SELECT * FROM Groups WHERE Groups.Id = @groupId";

            using (var con = _context.CreateConnection())
            {
                var result = await con.QueryFirstOrDefaultAsync<Group>(query, new { groupId = groupId });
                return result;
            }
        }
    }
}
