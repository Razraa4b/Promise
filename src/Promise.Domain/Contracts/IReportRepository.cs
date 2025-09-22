using Promise.Domain.Models;

namespace Promise.Domain.Contracts
{
    public interface IReportRepository : IRepository<Report>
    {
        public Task<Report?> GetByTitle(string title);
    }
}
