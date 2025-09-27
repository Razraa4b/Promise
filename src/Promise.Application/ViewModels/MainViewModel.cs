using Autofac;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<MainViewModel> _logger;
        private readonly ILifetimeScope _scope;

        public RoutingState Router { get; } = new RoutingState();

        public ReactiveCommand<string, IRoutableViewModel?> NavigateViewCommand { get; }

        public MainViewModel(ILifetimeScope scope, ILogger<MainViewModel> logger)
        {
            _scope = scope;
            _logger = logger;

            NavigateViewCommand = ReactiveCommand.CreateFromObservable<string, IRoutableViewModel?>(
                param => NavigateToView(param)
            );
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
