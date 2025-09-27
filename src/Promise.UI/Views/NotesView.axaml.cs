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
        private const double EdgeOffset = 20;
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

            MainDialog.OnDialogClosed += d => TitleTextBox.Clear();
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

        private void GridSplitter_DragDelta(object? sender, Avalonia.Input.VectorEventArgs e)
        {
            if (hostWindow == null) return;

            double currentWidth = MainGrid.ColumnDefinitions[0].Width.Value;

            // Resize of the column if it tries to increase the size beyond the limit.
            if (currentWidth > hostWindow.Width - EdgeOffset)
            {
                MainGrid.ColumnDefinitions[0].Width = new GridLength(hostWindow.Width - EdgeOffset);
            }
        }
    }
}
