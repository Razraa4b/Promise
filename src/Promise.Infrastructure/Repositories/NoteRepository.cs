using Microsoft.EntityFrameworkCore;
using Promise.Domain.Contracts;
using Promise.Domain.Models;
using Promise.Infrastructure.Database;

namespace Promise.Infrastructure.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly ApplicationContext _context;

        public NoteRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task Add(Note entity)
        {
            await _context.Notes.AddAsync(entity);
        }

        public async Task Delete(Note entity)
        {
            await Task.Run(() => _context.Notes.Remove(entity));
        }

        public async Task<Note?> GetById(int id)
        {
            return await _context.Notes.FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<Note?> GetByTitle(string title)
        {
            return await _context.Notes.FirstOrDefaultAsync(n => n.Title == title);
        }

        public Task<IQueryable<Note>> GetAll()
        {
            return Task.FromResult(_context.Notes.AsQueryable());
        }

        public async Task Update(Note entity)
        {
            await Task.Run(() => _context.Notes.Update(entity));
        }
    }
}
