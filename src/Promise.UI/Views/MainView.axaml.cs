using Avalonia.ReactiveUI;
using Promise.Application.ViewModels;
using System;

namespace Promise.UI.Views
{
    public partial class MainView : ReactiveWindow<MainViewModel>
    {
        public MainView()
        {
            InitializeComponent();

            if (OperatingSystem.IsLinux())
            {
                TitleBar.IsVisible = false;
            }
        }
    }
}