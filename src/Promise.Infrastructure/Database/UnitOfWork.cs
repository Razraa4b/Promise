using Microsoft.EntityFrameworkCore.Storage;
using Promise.Domain.Contracts;
using Promise.Infrastructure.Repositories;

namespace Promise.Infrastructure.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        private IDbContextTransaction _transaction;

        public INoteRepository NoteRepository { get; init; }
        public IReportRepository ReportRepository { get; init; }

        UnitOfWork(ApplicationContext context)
        {
            _context = context;

            NoteRepository = new NoteRepository(context);
            ReportRepository = new ReportRepository(context);

            _transaction = _context.Database.BeginTransaction();
        }

        public async Task Commit()
        {
            await _transaction.CommitAsync();
        }

        public async Task Rollback()
        {
            await _transaction.RollbackAsync();
        }
    }
}
