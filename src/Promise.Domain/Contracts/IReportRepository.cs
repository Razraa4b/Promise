using Promise.Domain.Entities;

namespace Promise.Domain.Contracts
{
    public interface IReportRepository : IRepository<Report>
    {
        public Task<Report?> GetByTitle(string title);
    }
}
