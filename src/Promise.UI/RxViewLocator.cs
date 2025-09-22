using Autofac;
using Promise.Application.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;

namespace Promise.UI
{
    /// <summary>
    /// View locator for ReactiveUI
    /// </summary>
    public class RxViewLocator : IViewLocator
    {
        private readonly ILifetimeScope? _scope = null;
        private readonly Dictionary<Type, IViewFor> _viewMappings;

        public RxViewLocator(ILifetimeScope scope)
        {
            _scope = scope;

            _viewMappings = new Dictionary<Type, IViewFor>()
            {
                { typeof(NotesViewModel), _scope.Resolve<IViewFor<NotesViewModel>>() },
                { typeof(ReportsViewModel), _scope.Resolve<IViewFor<ReportsViewModel>>() }
            };
        }

        public IViewFor? ResolveView<T>(T? viewModel, string? contract = null)
        {
            if (_scope is null) throw new NullReferenceException(nameof(_scope));

            Type viewModelType = viewModel?.GetType() ?? typeof(T);
            if (_viewMappings.TryGetValue(viewModelType, out IViewFor? view)) return view;
            return null;
        }
    }
}
