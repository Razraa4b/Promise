using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
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
                if(hostWindow != null)
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

                if (newWidth > 0 && newWidth <= maxWidth)
                {
                    MainGrid.ColumnDefinitions[0].Width = new GridLength(newWidth);
                }
            }
        }
    }
}