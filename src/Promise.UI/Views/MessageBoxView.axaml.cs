using Avalonia.ReactiveUI;
using Promise.Application.ViewModels;

namespace Promise.UI;

public partial class MessageBoxView : ReactiveWindow<MessageBoxViewModel>
{
    public MessageBoxView()
    {
        InitializeComponent();
    }
}