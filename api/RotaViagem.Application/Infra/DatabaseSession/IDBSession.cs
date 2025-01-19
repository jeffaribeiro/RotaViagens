using System.Data.Common;

namespace RotaViagem.Application.Infra.DatabaseSession
{
    public interface IDBSession : IDisposable
    {
        DbConnection Connection { get; }
        DbTransaction Transaction { get; }
        void BeginTransaction();
        Task BeginTransactionAsync();
    }
}
