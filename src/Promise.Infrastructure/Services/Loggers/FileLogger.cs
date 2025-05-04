using Promise.Domain.Contracts;
using Promise.Domain.Enums;
using System.Globalization;

namespace Promise.Infrastructure.Services.Loggers
{
    public class FileLogger<T> : ILogger<T>
    {
        private string owner;

        public string FileName { get; set; } = "PromiseApp.log";

        public FileLogger()
        {
            owner = typeof(T).Name;
        }

        private string GetFormattedLogMessage(LogLevel level, object? message)
        {
            string date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.ffff", CultureInfo.InvariantCulture);
            string formattedMessage = string.Format("[{0}] {1}-{2}: {3}", date, level, owner, message);
            return formattedMessage;
        }

        public void Log(LogLevel level, object? message)
        {
            using (StreamWriter writer = new StreamWriter(FileName, true))
            {
                writer.WriteLine(GetFormattedLogMessage(level, message));
            }
        }

        public async Task LogAsync(LogLevel level, object? message)
        {
            using (StreamWriter writer = new StreamWriter(FileName, true))
            {
                await writer.WriteLineAsync(GetFormattedLogMessage(level, message));
            }
        }
    }
}
