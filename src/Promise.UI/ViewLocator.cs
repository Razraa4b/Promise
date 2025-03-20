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
            switch (viewModel)
            {
                case NotesViewModel:
                    return new NotesView() { DataContext = viewModel };
                case ReportsViewModel:
                    return new ReportsView() { DataContext = viewModel };
                default:
                    return null;
            }
        }
    }
}
