using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.VisualTree;
using ReactiveUI;
using System;

namespace Promise.UI.Controls
{
    public partial class TitleBar : UserControl, IActivatableView
    {
        private Window? hostWindow;

        // Icon Property
        public static readonly DirectProperty<TitleBar, object?> IconProperty = AvaloniaProperty.RegisterDirect<TitleBar, object?>(
            nameof(Icon),
            o => o.Icon,
            (o, s) => o.Icon = s
        );

        private object? icon;
        public object? Icon
        {
            get => icon;
            set => SetAndRaise(IconProperty, ref icon, value);
        }

        // Title Property
        public static readonly DirectProperty<TitleBar, string> TitleProperty = AvaloniaProperty.RegisterDirect<TitleBar, string>(
            nameof(Title),
            o => o.Title,
            (o, s) => o.Title = s
        );

        private string title = "Window";
        public string Title
        {
            get => title;
            set => SetAndRaise(TitleProperty, ref title, value);
        }

        // Can Resize Property
        public static readonly DirectProperty<TitleBar, bool> CanResizeProperty = AvaloniaProperty.RegisterDirect<TitleBar, bool>(
            nameof(CanResize),
            o => o.CanResize,
            (o, s) => o.CanResize = s
        );

        private bool canResize = true;
        public bool CanResize
        {
            get => canResize;
            set => SetAndRaise(CanResizeProperty, ref canResize, value);
        }

        public TitleBar()
        {
            InitializeComponent();

            this.WhenActivated(disposables =>
            {
                hostWindow = this.GetVisualRoot() as Window;
                if (hostWindow != null)
                {
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
            });
        }

        private void MinimizeButtonClick(object? sender, RoutedEventArgs e)
        {
            if (hostWindow == null) return;
            hostWindow.WindowState = WindowState.Minimized;
        }

        private void MaximizeButtonClick(object? sender, RoutedEventArgs e)
        {
            if (hostWindow == null) return;

            if (hostWindow.WindowState == WindowState.Maximized)
                hostWindow.WindowState = WindowState.Normal;
            else if (hostWindow.WindowState == WindowState.Normal)
                hostWindow.WindowState = WindowState.Maximized;
        }

        private void CloseButtonClick(object? sender, RoutedEventArgs e)
        {
            if (hostWindow != null)
                hostWindow.Close();
        }
    }
}