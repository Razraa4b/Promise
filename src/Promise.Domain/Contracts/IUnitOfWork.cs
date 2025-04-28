namespace Promise.Domain.Contracts
{
    public interface IUnitOfWork<T>
    {
        IRepository<T> GetRepository();
        Task SaveChanges();
    }
}
