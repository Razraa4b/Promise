using Microsoft.EntityFrameworkCore;
using Promise.Domain.Contracts;
using Promise.Domain.Models;
using Promise.Infrastructure.Database;

namespace Promise.Infrastructure.Repositories
{
    public class ReportsRepository : IRepository<Report>
    {
        private readonly ApplicationContext _context;

        public ReportsRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task Add(Report entity)
        {
            _context.Reports.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Report entity)
        {
            _context.Reports.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Report entity)
        {
            _context.Reports.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Report?> Get(Func<Report, bool> condition)
        {
            Report? report = await _context.Reports.FirstOrDefaultAsync(n => condition(n));
            return report;
        }

        public async Task<IEnumerable<Report>> GetAll(Func<Report, bool> condition)
        {
            List<Report> reports = await _context.Reports.Where(n => condition(n)).ToListAsync();
            return reports;
        }
    }
}
