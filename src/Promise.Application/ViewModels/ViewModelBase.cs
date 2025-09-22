using ReactiveUI;

namespace Promise.Application.ViewModels
{
    public abstract class ViewModelBase : ReactiveObject, IActivatableViewModel
    {
        public virtual ViewModelActivator Activator { get; } = new ViewModelActivator();
    }
}
