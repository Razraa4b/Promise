using Promise.Application.ViewModels;
using Promise.Domain.Contracts;
using Promise.Domain.Models;

namespace Promise.UI.Services
{
    public class MessageBoxService : INotificationService
    {
        public void Notify(Notification notification)
        {
            MessageBoxView messageBox = new MessageBoxView()
            {
                DataContext = new MessageBoxViewModel(notification)
            };

            messageBox.Show();
        }
    }
}
