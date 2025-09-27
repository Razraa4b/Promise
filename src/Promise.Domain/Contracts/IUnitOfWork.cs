namespace Promise.Domain.Contracts
{
    public interface IUnitOfWork : IAsyncDisposable, IDisposable
    {
        INoteRepository NoteRepository { get; }
        IReportRepository ReportRepository { get; }

        Task Begin(CancellationToken token = default);
        Task Commit(CancellationToken token = default);
        Task Rollback(CancellationToken token = default);
    }
}
