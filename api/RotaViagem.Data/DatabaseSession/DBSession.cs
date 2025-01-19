using RotaViagem.Application.Infra.DatabaseSession;
using System.Data.Common;

namespace RotaViagem.Data.DatabaseSession
{
    public class DBSession : IDBSession
    {
        public DbConnection Connection { get; }
        public DbTransaction Transaction { get; private set; }

        public DBSession(DbConnection connection)
        {
            Connection = connection;
            Connection.Open();
        }

        public void BeginTransaction()
        {
            if (Transaction is null)
                Transaction = Connection.BeginTransaction();
        }

        public async Task BeginTransactionAsync()
        {
            if (Transaction is null)
                Transaction = await Connection.BeginTransactionAsync();
        }

        public void Dispose()
        {
            Connection?.Dispose();
        }
    }
}
