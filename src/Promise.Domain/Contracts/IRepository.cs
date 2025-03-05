namespace Promise.Domain.Contracts
{
    public interface IRepository<T>
    {
        Task Add(T entity);
        Task Delete(T entity);
        Task Update(T entity);
        Task<T?> Get(Func<T, bool> condition);
        Task<IEnumerable<T>> GetAll(Func<T, bool> condition);
    }
}
