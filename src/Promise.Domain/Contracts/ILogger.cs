using Promise.Domain.Enums;

namespace Promise.Domain.Contracts
{
    public interface ILogger<T>
    {
        void Log(LogLevel level, object? message);
        Task LogAsync(LogLevel level, object? message);
    }
}
