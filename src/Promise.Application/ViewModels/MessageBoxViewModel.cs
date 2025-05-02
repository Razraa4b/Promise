using Promise.Domain.Models;

namespace Promise.Application.ViewModels
{
    public class MessageBoxViewModel : BaseViewModel
    {
        public Notification NotificationData { get; set; }

        public MessageBoxViewModel(Notification notification)
        {
            NotificationData = notification;
        }
    }
}
