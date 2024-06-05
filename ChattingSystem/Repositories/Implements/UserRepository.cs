using ChattingSystem.Data;
using ChattingSystem.Models;
using ChattingSystem.Repositories.Interfaces;
using Dapper;

namespace ChattingSystem.Repositories.Implements
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperDbContext _context;
        public UserRepository(DapperDbContext context)
        {
            _context = context;
        }
        public async Task<User>? GetById(int? Id)
        {
            string query = "SELECT * FROM Users WHERE Id = @id";
            using (var connection = _context.CreateConnection())
            {
                var emp = await connection.QueryFirstOrDefaultAsync<User>(query, new { Id });
                return emp;
            }
        }

        public async Task<IEnumerable<User>>? GetAllUsersExceptOneSpecific(int? userId)
        {
            string query = "SELECT * FROM Users WHERE NOT Id = @userId";
            using (var connections = _context.CreateConnection())
            {
                var users = await connections.QueryAsync<User>(query, new { userId });
                return users;
            }
        }

    }
}

