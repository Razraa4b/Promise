using Avalonia.Controls;
using Avalonia.ReactiveUI;
using Avalonia.VisualTree;
using Promise.Application.ViewModels;
using ReactiveUI;
using System;

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
                if (hostWindow != null)
                {
                    hostWindow.Resized += HostWindowResized;
                }
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
    }
}
