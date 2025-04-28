using Promise.Domain.Contracts;
using Promise.Domain.Enums;

namespace Promise.Infrastructure.Database
{
    public class UnitOfWork<T> : IUnitOfWork<T>
    {
        private readonly ApplicationContext _context;
        private readonly IRepository<T> _repository;
        private readonly ILogger<UnitOfWork<T>> _logger;

        public UnitOfWork(ApplicationContext context, IRepository<T> repository, ILogger<UnitOfWork<T>> logger)
        {
            _context = context;
            _repository = repository;
            _logger = logger;
        }

        public IRepository<T> GetRepository()
        {
            return _repository;
        }

        public async Task SaveChanges()
        {
            _logger.Log(LogLevel.Debug, "Save changes to database");
            await _context.SaveChangesAsync();
        }
    }
}
