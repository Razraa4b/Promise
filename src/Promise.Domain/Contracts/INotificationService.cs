using Promise.Domain.Models;

namespace Promise.Domain.Contracts
{
    public interface INotificationService
    {
        void Notify(Notification notification);
    }
}
