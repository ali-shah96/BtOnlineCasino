using OnlineCasino.DatabaseContext.Entities;

namespace OnlineCasino.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        Task<IEnumerable<Users>> GetUsers();
        Task<Users> GetUser(int id);
        Task<Users> CreateUser(Users user);
        Task<bool> UpdateUser(int id, Users user);
        Task<bool> DeleteUser(int id);
        bool IsValidCredentials(string username, string password);
    }
}
