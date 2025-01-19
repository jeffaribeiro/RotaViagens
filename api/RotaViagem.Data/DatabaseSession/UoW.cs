using RotaViagem.Application.Infra.DatabaseSession;
using RotaViagem.Application.Infra.Repositories;
using RotaViagem.Data.Repositories;
using System.Data.Common;

namespace RotaViagem.Data.DatabaseSession
{
    public sealed class UoW : IUoW, IDisposable
    {
        public IDBSession Session { get; }

        public IRotaRepository RotaRepository => new RotaRepository(Session);

        public UoW(IDBSession session)
        {
            Session = session;
        }

        public DbTransaction BeginTransaction()
        {
            if (Session.Transaction is null)
                Session.BeginTransaction();

            return Session.Transaction;
        }

        public bool Commit()
        {
            try
            {
                Session.Transaction.Commit();

                if (Session.Transaction != null)
                {
                    Console.WriteLine("Commit bem-sucedido!");
                    return true;
                }
                else
                {
                    Console.WriteLine("O commit falhou. A transação foi revertida.");
                    Rollback();
                    return false;
                }
            }
            catch
            {
                Console.WriteLine("Erro inesperado durante o commit. A transação foi revertida.");
                Rollback();
                return false;
            }
        }

        public void Rollback()
        {
            Session.Transaction.Rollback();
            Dispose();
        }

        public void Dispose()
        {
            Session.Connection.Close();
            Session.Connection.Dispose();
        }

        public async Task<DbTransaction> BeginTransactionAsync()
        {
            if (Session.Transaction is null)
                await Session.BeginTransactionAsync();

            return Session.Transaction;
        }

        public async Task<bool> CommitAsync()
        {
            try
            {
                await Session.Transaction.CommitAsync();

                if (Session.Transaction != null)
                {
                    Console.WriteLine("Commit bem-sucedido!");
                    await DisposeAsync();
                    return true;
                }
                else
                {
                    Console.WriteLine("A transação foi revertida. O commit falhou.");
                    await DisposeAsync();
                    return false;
                }
            }
            catch
            {
                Console.WriteLine("Erro durante o commit. A transação foi revertida.");
                await RollbackAsync();
                return false;
            }

        }

        public async Task RollbackAsync()
        {
            await Session.Transaction.RollbackAsync();
            await DisposeAsync();
        }

        public async Task DisposeAsync()
        {
            await Session.Connection.CloseAsync();
            await Session.Connection.DisposeAsync();
        }
    }
}
