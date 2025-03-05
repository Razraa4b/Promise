using Promise.Domain.Enums;

namespace Promise.Domain.Contracts
{
    public interface ILogger<T>
    {
        public void Log(LogLevel level, object? message);
    }
}
