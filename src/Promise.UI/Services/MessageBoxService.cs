using Autofac;
using Promise.Application.ViewModels;
using Promise.Domain.Contracts;
using Promise.Domain.Models;
using Promise.UI.Views;

namespace Promise.UI.Services
{
    public class MessageBoxService : INotificationService
    {
        public void Notify(Notification notification)
        {
            var messageBox = new MessageBoxView()
            {
                DataContext = new MessageBoxViewModel(notification)
            };

            messageBox.Show();
        }
    }
}
