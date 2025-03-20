using ReactiveUI;

namespace Promise.Application.ViewModels
{
    public abstract class BaseViewModel : ReactiveObject, IActivatableViewModel
    {
        public virtual ViewModelActivator Activator { get; } = new ViewModelActivator();
    }
}
