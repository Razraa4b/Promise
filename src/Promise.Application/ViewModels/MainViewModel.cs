using Autofac;
using ReactiveUI;
using System.Reactive.Disposables;

namespace Promise.Application.ViewModels
{
    public class MainViewModel : ViewModelBase, IScreen
    {
        private readonly Dictionary<string, Type> _viewModelTypes = new()
        {
            ["Notes"] = typeof(NotesViewModel),
            ["Reports"] = typeof(ReportsViewModel)
        };
        private readonly ILifetimeScope _scope;

        public RoutingState Router { get; } = new RoutingState();

        public ReactiveCommand<string, IRoutableViewModel?> NavigateViewCommand { get; }

        public MainViewModel(ILifetimeScope scope)
        {
            _scope = scope;

            NavigateViewCommand = ReactiveCommand.CreateFromObservable<string, IRoutableViewModel?>(
                param => NavigateToView(param)
            );

            this.WhenActivated((CompositeDisposable disposables) =>
            {
                NavigateViewCommand.Execute("Notes");
            });
        }

        private IObservable<IRoutableViewModel?> NavigateToView(string viewName)
        {
            if (_viewModelTypes.TryGetValue(viewName, out Type? viewModelType))
            {
                if (_scope.TryResolve(viewModelType, out object? instance) &&
                    instance is IRoutableViewModel viewModel)
                {
                    return Router.Navigate.Execute(viewModel);
                }
            }
            throw new NullReferenceException("Navigate to `null` view is impossible");
        }
    }
}
