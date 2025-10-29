using Domain.Entities;
namespace Application.Services;

public interface IUserService
{
    Task<User?> GetUserByIdAsync(int id);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<int> CreateUserAsync(User user);
    Task<bool> UpdateUserAsync(User user);
    Task<bool> DeleteUserAsync(int id);
    Task<IEnumerable<User>> GetUsersByUserNameAsync(string userName);
    Task CreateUsersInTransactionAsync(IEnumerable<User> users);
}
