using Avalonia.ReactiveUI;
using Promise.Application.ViewModels;
using System;

namespace Promise.UI.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();

            if (!OperatingSystem.IsWindows())
            {
                TitleBar.IsVisible = false;
            }
        }
    }
}