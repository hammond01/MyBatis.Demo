using Domain.Entities;

namespace Infrastructure.Mappers;

public interface IUserMapper
{
    List<User> GetAll();
    User? GetById(int id);
    int InsertUser(User user);
    int UpdateUser(int id, string userName, string email);
    int DeleteUser(int id);
    List<User> GetByUserName(string value);
}
