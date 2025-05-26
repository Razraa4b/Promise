using Autofac;
using Avalonia.Controls;
using Promise.Domain.Contracts;
using ReactiveUI;
using System;
using System.Threading.Tasks;

namespace Promise.UI.Services
{
    public class ViewManager : IViewManager
    {
        private readonly ILifetimeScope _scope;

        public ViewManager(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public IViewFor<TViewModel> GetViewByViewModel<TViewModel>() where TViewModel : class
        {
            return _scope.Resolve<IViewFor<TViewModel>>();
        }

        public IViewFor<TViewModel> GetViewByViewModel<TViewModel>(TViewModel viewModel) where TViewModel : class
        {
            var view = _scope.Resolve<IViewFor<TViewModel>>();
            view.ViewModel = viewModel;
            
            return view;
        }

        public void ShowView<TView>(IViewFor<TView> view) where TView : class
        {
            if(view is Window window)
            {
                window.Show();
            }
        }

        public async Task<object?> ShowViewDialog<TViewOwner, TView>(IViewFor<TViewOwner> owner, IViewFor<TView> view)
            where TViewOwner : class
            where TView : class
        {
            if(view is Window window && owner is Window ownerWindow)
            {
                object? result = await window.ShowDialog<object?>(ownerWindow);
                return result;
            }
            return null;
        }

        public void CloseView<TView>(IViewFor<TView> view) where TView : class
        {
            if(view is Window window)
            {
                window.Close();
            }
        }
    }
}
