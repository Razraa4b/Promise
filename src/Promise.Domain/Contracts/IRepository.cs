namespace Promise.Domain.Contracts
{
    public interface IRepository<TEntity>
    {
        Task Add(TEntity entity);
        Task Delete(TEntity entity);
        Task Update(TEntity entity);
        Task<TEntity?> GetById(int id);
        Task<IEnumerable<TEntity>> GetAll();
        Task Save();
    }
}
