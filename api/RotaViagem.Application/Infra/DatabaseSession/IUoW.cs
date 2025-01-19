using RotaViagem.Application.Infra.Repositories;
using System.Data.Common;

namespace RotaViagem.Application.Infra.DatabaseSession
{
    public interface IUoW : IDisposable
    {
        IDBSession Session { get; }

        IRotaRepository RotaRepository { get; }

        DbTransaction BeginTransaction();
        bool Commit();
        void Rollback();
        Task<DbTransaction> BeginTransactionAsync();
        Task<bool> CommitAsync();
        Task RollbackAsync();
        Task DisposeAsync();
    }
}
