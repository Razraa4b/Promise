using Promise.Domain.Contracts;
using Promise.Domain.Enums;

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

        public void Log(LogLevel level, object? message)
        {
            string formattedMessage = string.Format("[{0}] {1}:{2}:: {3}", DateTime.Now, level, owner, message);
            using (StreamWriter writer = new StreamWriter(FileName, true))
            {
                writer.WriteLine(formattedMessage);
            }
        }
    }
}
