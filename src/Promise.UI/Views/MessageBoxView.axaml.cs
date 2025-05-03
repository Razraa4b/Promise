using Avalonia.ReactiveUI;
using Promise.Application.ViewModels;
using System;

namespace Promise.UI;

public partial class MessageBoxView : ReactiveWindow<MessageBoxViewModel>
{
    public MessageBoxView()
    {
        InitializeComponent();

        if (OperatingSystem.IsLinux())
        {
            TitleBar.IsVisible = false;
        }
    }
}