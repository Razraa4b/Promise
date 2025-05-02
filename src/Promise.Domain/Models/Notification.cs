using Promise.Domain.Enums;

namespace Promise.Domain.Models
{
    public class Notification
    {
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public NotificationType Type { get; set; }
    }
}
