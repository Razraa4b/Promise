using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.ReactiveUI;
using Avalonia.VisualTree;
using Promise.Application.ViewModels;
using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Promise.UI.Views
{
    public partial class NotesView : ReactiveUserControl<NotesViewModel>
    {
        private const double EdgeOffset = 10;
        private Window? hostWindow;

        public NotesView()
        {
            InitializeComponent();

            this.WhenActivated(disposables =>
            {
                hostWindow = this.GetVisualRoot() as Window;
                if(hostWindow != null)
                {
                    hostWindow.Resized += HostWindowResized;
                }

                this.WhenAnyValue(v => v.ViewModel!.SelectedNote)
                    .Select(note => note != null)
                    .BindTo(this, v => v.DeleteButton.IsEnabled)
                    .DisposeWith(disposables);
            });
        }

        private void HostWindowResized(object? sender, WindowResizedEventArgs e)
        {
            double currentWidth = MainGrid.ColumnDefinitions[0].Width.Value;
            double maxWidth = e.ClientSize.Width - EdgeOffset;
            double newWidth = Math.Min(currentWidth, maxWidth);

            // Resize the column if it exceeds the limits of available space in the window
            if (Math.Abs(newWidth - currentWidth) > 0)
            {
                MainGrid.ColumnDefinitions[0].Width = new GridLength(newWidth);
            }
        }

        private void GridSplitterDragDelta(object? sender, VectorEventArgs e)
        {
            if (hostWindow != null)
            {
                double maxWidth = hostWindow.ClientSize.Width - EdgeOffset;
                double newWidth = MainGrid.ColumnDefinitions[0].Width.Value + e.Vector.X;

                // Resize the column if there is still space in the window
                if (newWidth > 0 && newWidth <= maxWidth)
                {
                    MainGrid.ColumnDefinitions[0].Width = new GridLength(newWidth);
                }
            }
        }
    }
}
