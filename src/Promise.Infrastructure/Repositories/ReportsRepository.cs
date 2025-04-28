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
            await _context.Reports.AddAsync(entity);
        }

        public Task Delete(Report entity)
        {
            _context.Reports.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<Report?> Get(int id)
        {
            return await _context.Reports.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Report>> GetAll()
        {
            return await _context.Reports.ToListAsync();
        }

        public Task Update(Report entity)
        {
            _context.Reports.Update(entity);
            return Task.CompletedTask;
        }
    }
}
