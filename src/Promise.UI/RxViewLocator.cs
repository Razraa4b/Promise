using Autofac;
using Promise.Application.ViewModels;
using ReactiveUI;
using System;

namespace Promise.UI
{
    /// <summary>
    /// View locator for ReactiveUI
    /// </summary>
    public class RxViewLocator : IViewLocator
    {
        private readonly ILifetimeScope? _scope = null;

        public RxViewLocator(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public IViewFor? ResolveView<T>(T? viewModel, string? contract = null)
        {
            if (_scope is null) throw new NullReferenceException(nameof(_scope));

            switch (viewModel)
            {
                case NotesViewModel:
                    return _scope.Resolve<IViewFor<NotesViewModel>>();
                case ReportsViewModel:
                    return _scope.Resolve<IViewFor<ReportsViewModel>>();
                default:
                    return null;
            }
        }
    }
}
