using Autofac;
using Microsoft.Extensions.Logging;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Disposables;

namespace Promise.Application.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IScreen
    {
        private readonly Dictionary<string, Type> _viewModelTypes = new()
        {
            ["Notes"] = typeof(NotesViewModel),
            ["Reports"] = typeof(ReportsViewModel)
        };
        private readonly ILogger<MainWindowViewModel> _logger;
        private readonly ILifetimeScope _scope;

        public RoutingState Router { get; init; } = new RoutingState();

        public ReactiveCommand<string, IRoutableViewModel?> NavigateViewCommand { get; }
        public ReactiveCommand<Unit, IRoutableViewModel> GoBack => Router.NavigateBack;

        public MainWindowViewModel(ILifetimeScope scope, ILogger<MainWindowViewModel> logger)
        {
            _scope = scope;
            _logger = logger;

            NavigateViewCommand = ReactiveCommand.CreateFromObservable<string, IRoutableViewModel?>(
                viewName => NavigateToView(viewName)
            );

            this.WhenActivated((CompositeDisposable disposables) =>
            {
                Router.ThrownExceptions
                    .Subscribe(ex => _logger.LogDebug($"Occursed exception on navigation: {ex}"));

                NavigateToView("Notes");
            });
        }

        private IObservable<IRoutableViewModel?> NavigateToView(string viewName)
        {
            if (_viewModelTypes.TryGetValue(viewName, out Type? viewModelType))
            {
                if (_scope.TryResolve(viewModelType, out object? instance) &&
                    instance is IRoutableViewModel viewModel)
                {
                    _logger.LogDebug($"Navigate to View: \'{viewName}\', with view-model: \'{viewModelType.Name}\'");
                    return Router.Navigate.Execute(viewModel);
                }
            }
            throw new NullReferenceException("Navigate to `null` view is impossible");
        }
    }
}
