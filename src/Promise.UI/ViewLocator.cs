using Autofac;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Promise.Application.ViewModels;
using Promise.UI.Views;
using ReactiveUI;
using System;

namespace Promise.UI
{
    public class ViewLocator : IDataTemplate, IViewLocator
    {
        private readonly ILifetimeScope? _scope = null;

        public ViewLocator() { }

        public ViewLocator(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public Control? Build(object? param)
        {
            if (param is null)
                return null;

            string name = param.GetType().FullName!.Replace("ViewModel", "View", StringComparison.Ordinal);
            Type? type = Type.GetType(name);

            if (type != null)
            {
                return (Control)Activator.CreateInstance(type)!;
            }

            return new TextBlock { Text = "Not Found: " + name };
        }

        public bool Match(object? data)
        {
            return data is BaseViewModel;
        }

        // For ReactiveUI
        public IViewFor? ResolveView<T>(T? viewModel, string? contract = null)
        {
            if (_scope is null) throw new NullReferenceException(nameof(_scope));

            switch (viewModel)
            {
                case NotesViewModel:
                    return _scope.Resolve<IViewFor<NotesViewModel>>();
                case ReportsViewModel:
                    return _scope.Resolve< IViewFor<ReportsViewModel>>();
                default:
                    return null;
            }
        }
    }
}
