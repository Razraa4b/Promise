using Avalonia.ReactiveUI;
using Promise.Application.ViewModels;
using System;

namespace Promise.UI.Views
{
    public partial class MainWindow : ReactiveWindow<MainViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();

            if (OperatingSystem.IsLinux())
            {
                TitleBar.IsVisible = false;
            }
        }
    }
}