using Microsoft.EntityFrameworkCore;
using Promise.Domain.Contracts;
using Promise.Domain.Models;
using Promise.Infrastructure.Database;

namespace Promise.Infrastructure.Repositories
{
    public class NotesRepository : IRepository<Note>
    {
        private readonly ApplicationContext _context;

        public NotesRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task Add(Note entity)
        {
            _context.Notes.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Note entity)
        {
            _context.Notes.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Note entity)
        {
            _context.Notes.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Note?> Get(Func<Note, bool> condition)
        {
            Note? note = await _context.Notes.FirstOrDefaultAsync(n => condition(n));
            return note;
        }

        public async Task<IEnumerable<Note>> GetAll(Func<Note, bool> condition)
        {
            List<Note> notes = await _context.Notes.Where(n => condition(n)).ToListAsync();
            return notes;
        }
    }
}
