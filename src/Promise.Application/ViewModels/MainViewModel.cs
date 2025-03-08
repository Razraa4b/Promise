using Autofac;
using ReactiveUI;
using System.Reactive.Disposables;

namespace Promise.Application.ViewModels
{
    public class MainViewModel : BaseViewModel, IScreen, IActivatableViewModel
    {
        private readonly ILifetimeScope _scope;

        public RoutingState Router { get; } = new RoutingState();

        public ReactiveCommand<string, IRoutableViewModel?> NavigateViewCommand { get; set; }

        public ViewModelActivator Activator { get; set; } = new ViewModelActivator();

        public MainViewModel(ILifetimeScope scope)
        {
            _scope = scope;

            NavigateViewCommand = ReactiveCommand.CreateFromObservable<string, IRoutableViewModel?>((string param) =>
            {
                string viewModelName = "Promise.Application.ViewModels." + param.Replace(" ", "").Trim() + "ViewModel";

                Type? type = Type.GetType(viewModelName);

                if (type != null)
                {
                    _scope.TryResolve(type, out object? instance);

                    if (instance is IRoutableViewModel viewModel)
                       return Router.Navigate.Execute(viewModel);
                }
                throw new ArgumentException(null, nameof(param));
            });

            this.WhenActivated((CompositeDisposable disposables) =>
            {
                NavigateViewCommand.Execute("Notes");
            });
        }
    }
}
