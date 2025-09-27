using Microsoft.EntityFrameworkCore.Storage;
using Promise.Domain.Contracts;
using Promise.Infrastructure.Repositories;

namespace Promise.Infrastructure.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        private IDbContextTransaction? _transaction;
        private bool _disposed = false;

        public INoteRepository NoteRepository { get; }
        public IReportRepository ReportRepository { get; }

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            NoteRepository = new NoteRepository(context);
            ReportRepository = new ReportRepository(context);
        }

        public async Task Begin(CancellationToken token = default)
        {
            if (_transaction != null)
                throw new InvalidOperationException("Transaction already begun");

            _transaction = await _context.Database.BeginTransactionAsync(token);
        }

        public async Task Commit(CancellationToken token = default)
        {
            if (_transaction == null)
                throw new InvalidOperationException("No active transaction");

            try
            {
                await _context.SaveChangesAsync(token);
                await _transaction.CommitAsync(token);
            }
            finally
            {
                await CleanupTransaction();
            }
        }

        public async Task Rollback(CancellationToken token = default)
        {
            if (_transaction == null) return;

            try
            {
                await _transaction.RollbackAsync(token);
            }
            finally
            {
                await CleanupTransaction();
            }
        }

        private async Task CleanupTransaction()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (!_disposed)
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            if(!_disposed)
            {
                if(_transaction != null)
                {
                    _transaction.Dispose();
                }
                _disposed = true;
            }
        }
    }
}