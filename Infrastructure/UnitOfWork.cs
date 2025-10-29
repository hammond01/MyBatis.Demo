using Domain;
using MyBatis.NET.Core;
namespace Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly SqlSession _session;

    public UnitOfWork(SqlSession session)
    {
        _session = session;
    }

    public async Task BeginTransactionAsync()
    {
        await Task.Run(() => _session.BeginTransaction());
    }

    public async Task CommitAsync()
    {
        await Task.Run(() => _session.Commit());
    }

    public async Task RollbackAsync()
    {
        await Task.Run(() => _session.Rollback());
    }

    public async Task SaveChangesAsync()
    {
        await Task.Run(() => _session.Commit());
    }
}
