using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Mappers;
using MyBatis.NET.Core;
using MyBatis.NET.Mapper;

namespace Infrastructure.Repositories.Repository;

public class UserRepository : IUserRepository
{
    private readonly IUserMapper _mapper;

    public UserRepository(SqlSession session)
    {
        _mapper = session.GetMapper<IUserMapper>();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await Task.Run(() => _mapper.GetById(id));
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await Task.Run(() => _mapper.GetAll());
    }

    public async Task<int> CreateAsync(User user)
    {
        return await Task.Run(() => _mapper.InsertUser(user));
    }

    public async Task<bool> UpdateAsync(User user)
    {
        var result = await Task.Run(() => _mapper.UpdateUser(user.Id, user.UserName, user.Email));
        return result > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var result = await Task.Run(() => _mapper.DeleteUser(id));
        return result > 0;
    }

    public async Task<List<User>> GetByUserNameAsync(string userName)
    {
        return await Task.Run(() => _mapper.GetByUserName(userName));
    }
}
