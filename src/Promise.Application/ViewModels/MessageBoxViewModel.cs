using Promise.Domain.Enums;
using Promise.Domain.Models;
using ReactiveUI;

namespace Promise.Application.ViewModels
{
    public class MessageBoxViewModel : BaseViewModel
    {
        private Notification notificationData = new Notification("Message Box", "Content is empty", NotificationType.Info);
        public Notification NotificationData
        {
            get => notificationData;
            set
            {
                notificationData = value;
                this.RaisePropertyChanged(nameof(NotificationData));
            }
        }

        public MessageBoxViewModel(Notification notification)
        {
            NotificationData = notification;
        }
    }
}
