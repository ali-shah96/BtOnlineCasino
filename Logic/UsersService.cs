using Microsoft.EntityFrameworkCore;
using OnlineCasino.DatabaseContext.Entities;
using OnlineCasino.Logic.Interfaces;
using OnlineCasino.Pagination;
using OnlineCasino.Repositories.Interfaces;

namespace OnlineCasino.Logic
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _userRepository;

        public UsersService(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<(IEnumerable<Users> users, PaginationMetadata metadata)> GetUsers(int page, int pageSize)
        {
            var users = await _userRepository.GetUsers();
            var totalUsers =  users.Count();

            var paginationModel = new PaginationModel<Users>();
            var paginatedUsers = paginationModel.Paginate(users, page, pageSize);
            var metadata = paginationModel.GetPaginationMetadata(totalUsers, page, pageSize);

            return (paginatedUsers, metadata);
        }

        public async Task<Users> GetUser(int id)
        {
            return await _userRepository.GetUser(id);
        }

        public async Task<Users> CreateUser(Users user)
        {
            return await _userRepository.CreateUser(user);
        }

        public async Task<bool> UpdateUser(int id, Users user)
        {
            return await _userRepository.UpdateUser(id, user);
        }

        public async Task<bool> DeleteUser(int id)
        {
            return await _userRepository.DeleteUser(id);
        }

        public bool IsValidCredentials(string username, string password)
        {
            return _userRepository.IsValidCredentials(username, password);
        }
    }

}
