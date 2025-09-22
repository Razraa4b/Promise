namespace Promise.Domain.Contracts
{
    public interface IUnitOfWork
    {
        INoteRepository NoteRepository { get; }
        IReportRepository ReportRepository { get; }

        Task Commit();
        Task Rollback();
    }
}
