using Microsoft.EntityFrameworkCore.Storage;
using Promise.Domain.Contracts;
using Promise.Infrastructure.Repositories;

namespace Promise.Infrastructure.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        private IDbContextTransaction? _transaction;

        public INoteRepository NoteRepository { get; init; }
        public IReportRepository ReportRepository { get; init; }

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;

            NoteRepository = new NoteRepository(context);
            ReportRepository = new ReportRepository(context);
        }

        public async Task Begin(CancellationToken token = default)
        {
            if (_transaction == null)
            {
                _transaction = await _context.Database.BeginTransactionAsync(token);
            }
            else throw new InvalidOperationException("Transaction already begun");
        }

        public async Task Commit(CancellationToken token = default)
        {
            if (_transaction == null) return;

            await _context.SaveChangesAsync();
            await _transaction.CommitAsync(token);
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        public async Task Rollback(CancellationToken token = default)
        {
            if (_transaction == null) return;

            await _transaction.RollbackAsync(token);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
}
