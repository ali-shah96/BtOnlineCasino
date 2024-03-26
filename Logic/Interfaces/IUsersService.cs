using OnlineCasino.DatabaseContext.Entities;
using OnlineCasino.Pagination;

namespace OnlineCasino.Logic.Interfaces
{
    public interface IUsersService
    {
        Task<(IEnumerable<Users> users, PaginationMetadata metadata)> GetUsers(int page, int pageSize);
        Task<Users> GetUser(int id);
        Task<Users> CreateUser(Users user);
        Task<bool> UpdateUser(int id, Users user);
        bool IsValidCredentials(string username, string password);
        Task<bool> DeleteUser(int id);
    }
}