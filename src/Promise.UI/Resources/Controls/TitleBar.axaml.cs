using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace Promise.UI.Controls
{
    public partial class TitleBar : UserControl
    {
        public static readonly DirectProperty<TitleBar, string> TitleProperty = AvaloniaProperty.RegisterDirect<TitleBar, string>(
            nameof(Title),
            o => o.Title,
            (o, s) => o.Title = s
            );

        private string title = "Title Of The Window";
        public string Title
        {
            get => title;
            set
            {
                SetAndRaise(TitleProperty, ref title, value);
            }
        }

        public TitleBar()
        {
            InitializeComponent();

            SubscribeToWindowState();
        }

        private async void SubscribeToWindowState()
        {
            Window? hostWindow = (Window?)VisualRoot;

            while (hostWindow == null)
            {
                hostWindow = (Window?)VisualRoot;
                await Task.Delay(50);
            }

            hostWindow.GetObservable(Window.WindowStateProperty).Subscribe(s =>
            {
                if (s != WindowState.Maximized)
                {
                    MaximizeIcon.Data = Geometry.Parse("M2048 2048v-2048h-2048v2048h2048zM1843 1843h-1638v-1638h1638v1638z");
                    hostWindow.Padding = new Thickness(0);
                }
                if (s == WindowState.Maximized)
                {
                    MaximizeIcon.Data = Geometry.Parse("M2048 1638h-410v410h-1638v-1638h410v-410h1638v1638zm-614-1024h-1229v1229h1229v-1229zm409-409h-1229v205h1024v1024h205v-1229z");
                    hostWindow.Padding = new Thickness(7); 
                }
            });
        }

        private void MinimizeButtonClick(object? sender, RoutedEventArgs e)
        {
            Window? hostWindow = (Window?)VisualRoot;

            if (hostWindow != null)
                hostWindow.WindowState = WindowState.Minimized;
        }

        private void MaximizeButtonClick(object? sender, RoutedEventArgs e)
        {
            Window? hostWindow = (Window?)VisualRoot;

            if (hostWindow == null) return;

            if (hostWindow.WindowState == WindowState.Maximized)
                hostWindow.WindowState = WindowState.Normal;
            else if (hostWindow.WindowState == WindowState.Normal)
                hostWindow.WindowState = WindowState.Maximized;
        }

        private void CloseButtonClick(object? sender, RoutedEventArgs e)
        {
            Window? hostWindow = (Window?)VisualRoot;

            if (hostWindow != null)
                hostWindow.Close();
        }
    }
}