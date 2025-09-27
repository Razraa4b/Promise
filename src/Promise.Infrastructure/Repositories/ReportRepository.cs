using Microsoft.EntityFrameworkCore;
using Promise.Domain.Contracts;
using Promise.Domain.Entities;
using Promise.Infrastructure.Database;

namespace Promise.Infrastructure.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationContext _context;

        public ReportRepository(ApplicationContext context)
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

        public async Task<Report?> GetById(int id)
        {
            return await _context.Reports.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Report?> GetByTitle(string title)
        {
            return await _context.Reports.FirstOrDefaultAsync(r => r.Title == title);
        }

        public Task<IQueryable<Report>> GetAll()
        {
            return Task.FromResult(_context.Reports.AsQueryable());
        }

        public Task Update(Report entity)
        {
            _context.Reports.Update(entity);
            return Task.CompletedTask;
        }
    }
}
