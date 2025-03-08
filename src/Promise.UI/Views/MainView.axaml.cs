using Avalonia.ReactiveUI;
using Promise.Application.ViewModels;
using ReactiveUI;
using System;

namespace Promise.UI.Views
{
    public partial class MainView : ReactiveWindow<MainViewModel>
    {
        public MainView()
        {
            InitializeComponent();
        }
    }
}