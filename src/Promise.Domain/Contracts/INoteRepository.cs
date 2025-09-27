using Promise.Domain.Entities;

namespace Promise.Domain.Contracts
{
    public interface INoteRepository : IRepository<Note>
    {
        Task<Note?> GetByTitle(string title);
    }
}
